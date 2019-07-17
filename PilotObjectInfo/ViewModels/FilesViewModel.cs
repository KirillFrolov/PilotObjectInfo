using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Commands;
using Homebrew.Mvvm.Models;

namespace PilotObjectInfo.ViewModels
{
    class FilesViewModel : ObservableObject
    {
        private ReadOnlyCollection<IFile> _files;
        private IFileProvider _fileProvider;
        private RelayCommand _downloadCmd;
        private RelayCommand _downloadAllCmd;

        public FilesViewModel(ReadOnlyCollection<IFile> files, IFileProvider fileProvider)
        {
            _files = files;
            _fileProvider = fileProvider;

        }

        public ReadOnlyCollection<IFile> Files => _files;

        public RelayCommand DownloadCmd
        {
            get
            {
                if (_downloadCmd == null)
                {
                    _downloadCmd = new RelayCommand(DoDownLoad);
                }
                return _downloadCmd;

            }
        }


        public RelayCommand DownloadAllCmd
        {
            get
            {
                if (_downloadAllCmd == null)
                {
                    _downloadAllCmd = new RelayCommand(DoDownloadAllCmd, (o) => _files.Count() > 0);
                }
                return _downloadAllCmd;

            }
        }

        private void DoDownloadAllCmd(object obj)
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
                            using (FileStream output = new FileStream(Path.Combine(dialog.SelectedPath, file.Name), FileMode.Create))
                            {
                                stream.CopyTo(output);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void DoDownLoad(object obj)
        {
            var file = obj as IFile;
            if (file == null) return;
            var dlg = new Microsoft.Win32.SaveFileDialog();
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
                    System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }
    }
}
