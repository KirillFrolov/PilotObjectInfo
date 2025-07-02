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
using Ascon.Pilot.SDK;
using Microsoft.Win32;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class FilesViewModel : ReactiveObject
    {
        private Guid _objectId;
        private ReadOnlyCollection<IFile> _files;
        private IFileProvider _fileProvider;
        private FileModifier _fileModifier;
        private ReactiveCommand<IFile, Unit> _downloadCmd;
        private ReactiveCommand<Unit, Unit> _downloadAllCmd;
        private ReactiveCommand<Unit, Unit> _addFilesCmd;
        private ReactiveCommand<IFile, Unit> _delFileCmd;

        public FilesViewModel(Guid objectId, ReadOnlyCollection<IFile> files, IFileProvider fileProvider,
            FileModifier fileModifier = null)
        {
            _objectId = objectId;
            _files = files;
            _fileProvider = fileProvider;
            _fileModifier = fileModifier;
            Files = new ObservableCollection<IFile>(_files);
        }

        public ObservableCollection<IFile> Files { get; set; }

        private IFile _selectedFile;

        public IFile SelectedFile
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

        private string GetFileContent(IFile file)
        {
            using (var stream = _fileProvider.OpenRead(file))
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

        public ReactiveCommand<IFile, Unit> DownloadCmd
        {
            get
            {
                return _downloadCmd ?? (_downloadCmd = ReactiveCommand.Create<IFile, Unit>((f) =>
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

        public ReactiveCommand<IFile, Unit> DelFileCmd
        {
            get
            {
                return _delFileCmd ?? (_delFileCmd = ReactiveCommand.CreateFromTask<IFile, Unit>(async file =>
                {
                    await DoDelFile(file);
                    return Unit.Default;
                }, this.WhenAnyValue(vm => vm._fileModifier).Select(modifier => modifier != null)));
            }
        }

        private async Task DoDelFile(IFile file)
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

        private void Refresh(IEnumerable<IFile> files)
        {
            if (files == null) return;
            _files = new ReadOnlyCollection<IFile>(files.ToList());
            Files.Clear();
            _files.ToList().ForEach(x => Files.Add(x));
        }

        private void DoDownloadAllCmd()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK) return;

                foreach (var file in _files)
                {
                    using (var stream = _fileProvider.OpenRead(file))
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
                            System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void DoDownLoad(IFile file)
        {
            if (file == null) return;
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = Path.GetExtension(file.Name);
            dlg.FileName = file.Name;
            if (dlg.ShowDialog() != true) return;
            using (var stream = _fileProvider.OpenRead(file))
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
                    MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error);
                }
            }
        }
    }
}