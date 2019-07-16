using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class SnapshotsViewModel
    {
        public SnapshotsViewModel(IEnumerable<IFilesSnapshot> filesSnapshot, IFileProvider fileProvider)
        {
            Snapshots = new ObservableCollection<SnapshotViewModel>(filesSnapshot.Select(x => new SnapshotViewModel(x, fileProvider)));
        }

        public ObservableCollection<SnapshotViewModel> Snapshots { get; }
    }
}
