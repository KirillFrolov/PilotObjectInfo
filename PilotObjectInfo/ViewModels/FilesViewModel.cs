using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Services;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class FilesViewModel : ReactiveObject
    {
        private Guid _objectId;
        private List<PilotFile> _files;
        private FileService _fileService;
        private FileModifier _fileModifier;
        private ReactiveCommand<PilotFile, Unit> _downloadCmd;
        private ReactiveCommand<Unit, Unit> _downloadAllCmd;
        private ReactiveCommand<Unit, Unit> _addFilesCmd;
        private ReactiveCommand<PilotFile, Unit> _delFileCmd;

        public FilesViewModel(Guid objectId, List<PilotFile> files, FileService fileService,
            FileModifier fileModifier = null)
        {
            _objectId = objectId;
            _files = files ?? new List<PilotFile>();
            _fileService = fileService;
            _fileModifier = fileModifier;
            Files = new ObservableCollection<PilotFile>(_files);
        }

        public ObservableCollection<PilotFile> Files { get; set; }

        private PilotFile _selectedFile;

        public PilotFile SelectedFile
        {
            get => _selectedFile;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedFile, value);

                if (_selectedFile != null)
                {
                    SignnaturesInfo = new SignnaturesInfoViewModel(_selectedFile);
                    FileContent = GetFileContent(_selectedFile);
                }
                else
                {
                    SignnaturesInfo = null;
                }
            }
        }

        private string GetFileContent(PilotFile file)
        {
            using (var stream = _fileService.OpenRead(file.OriginalFile))
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                byte[] byteArray = memoryStream.ToArray();
                var str = Encoding.UTF8.GetString(byteArray);
                return str;
            }
        }

        private string _fileContent;

        public string FileContent
        {
            get => _fileContent;
            set => this.RaiseAndSetIfChanged(ref _fileContent, value);
        }

        private SignnaturesInfoViewModel _signnaturesInfo;

        public SignnaturesInfoViewModel SignnaturesInfo
        {
            get => _signnaturesInfo;
            set => this.RaiseAndSetIfChanged(ref _signnaturesInfo, value);
        }

        public ReactiveCommand<PilotFile, Unit> DownloadCmd
        {
            get
            {
                return _downloadCmd ?? (_downloadCmd = ReactiveCommand.Create<PilotFile, Unit>((f) =>
                {
                    DoDownLoad(f);
                    return Unit.Default;
                }));
            }
        }


        public ReactiveCommand<Unit, Unit> DownloadAllCmd
        {
            get
            {
                return _downloadAllCmd ?? (_downloadAllCmd = ReactiveCommand.Create<Unit, Unit>((_) =>
                {
                    DoDownloadAllCmd();
                    return Unit.Default;
                }, this.WhenAnyValue(vm => vm.Files.Count).Select(count => count > 0)));
            }
        }

        public ReactiveCommand<Unit, Unit> AddFilesCmd
        {
            get
            {
                return _addFilesCmd ?? (_addFilesCmd = ReactiveCommand.CreateFromTask(async _ =>
                {
                    await DoAddFiles();
                    return Unit.Default;
                }, this.WhenAnyValue(vm => vm._fileModifier).Select(modifier => modifier != null)));
            }
        }

        public ReactiveCommand<PilotFile, Unit> DelFileCmd
        {
            get
            {
                return _delFileCmd ?? (_delFileCmd = ReactiveCommand.CreateFromTask<PilotFile, Unit>(async file =>
                {
                    await DoDelFile(file);
                    return Unit.Default;
                }, this.WhenAnyValue(vm => vm._fileModifier).Select(modifier => modifier != null)));
            }
        }

        private async Task DoDelFile(PilotFile file)
        {
            if (file == null) return;
            if (MessageBox.Show($"Do you really want to delete a file: [{file.Name}]?", "Delete file",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var files = await _fileModifier.RemoveFile(_objectId, file);
                Refresh(files);
            }
        }

        private async Task DoAddFiles()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() != true) return;
            var files = await _fileModifier.AddFiles(_objectId, dialog.FileNames);
            Refresh(files);
        }

        private void Refresh(IEnumerable<PilotFile> files)
        {
            if (files == null) return;
            _files = files.ToList();
            Files.Clear();
            _files.ForEach(x => Files.Add(x));
        }

        private void DoDownloadAllCmd()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK) return;

                foreach (var file in _files)
                {
                    using (var stream = _fileService.OpenRead(file.OriginalFile))
                    {
                        try
                        {
                            using (FileStream output = new FileStream(Path.Combine(dialog.SelectedPath, file.Name),
                                       FileMode.Create))
                            {
                                stream.CopyTo(output);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void DoDownLoad(PilotFile file)
        {
            if (file == null) return;
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = Path.GetExtension(file.Name);
            dlg.FileName = file.Name;
            if (dlg.ShowDialog() != true) return;
            using (var stream = _fileService.OpenRead(file.OriginalFile))
            {
                try
                {
                    using (FileStream output = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        stream.CopyTo(output);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}