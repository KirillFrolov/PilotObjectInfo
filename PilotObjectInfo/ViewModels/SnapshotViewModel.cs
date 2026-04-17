using System;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Services;

namespace PilotObjectInfo.ViewModels
{
    class SnapshotViewModel
    {
        private FilesSnapshot _filesSnapshot;
        private FileService _fileService;
        private FilesViewModel _files;

        public SnapshotViewModel(Guid objectId, FilesSnapshot filesSnapshot, FileService fileService)
        {
            _filesSnapshot = filesSnapshot;
            _fileService = fileService;
            _files = new FilesViewModel(objectId, filesSnapshot.Files, fileService);
        }

        public long Version => _filesSnapshot.Created.Ticks;
        public DateTime Created => _filesSnapshot.Created;

        public int CreatorId => _filesSnapshot.CreatorId;

        public string Reason => _filesSnapshot.Reason;

        public FilesViewModel Files => _files;


    }
}
