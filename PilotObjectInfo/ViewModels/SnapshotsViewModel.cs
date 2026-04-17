using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Services;

namespace PilotObjectInfo.ViewModels
{
    class SnapshotsViewModel
    {
        public SnapshotsViewModel(Guid objectId, List<FilesSnapshot> filesSnapshots, FileService fileService)
        {
            Snapshots = new ObservableCollection<SnapshotViewModel>(
                filesSnapshots.Select(x => new SnapshotViewModel(objectId, x, fileService)));
        }

        public ObservableCollection<SnapshotViewModel> Snapshots { get; }
    }
}
