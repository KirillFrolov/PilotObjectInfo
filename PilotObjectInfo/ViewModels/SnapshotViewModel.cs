using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class SnapshotViewModel
    {
        private IFilesSnapshot _filesSnapshot;
        private IFileProvider _fileProvider;
        private FilesViewModel _files;

        public SnapshotViewModel(Guid objectId,  IFilesSnapshot filesSnapshot, IFileProvider fileProvider)
        {
            _filesSnapshot = filesSnapshot;
            _fileProvider = fileProvider;
            _files = new FilesViewModel(objectId, _filesSnapshot.Files, fileProvider);

        }

        public long Version => _filesSnapshot.Created.Ticks;
        public DateTime Created => _filesSnapshot.Created;

        public int CreatorId => _filesSnapshot.CreatorId;

        public string Reason => _filesSnapshot.Reason;

        public FilesViewModel Files => _files;


    }
}
