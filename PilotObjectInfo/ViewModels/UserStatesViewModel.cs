using System.Collections.Generic;
using System.Collections.ObjectModel;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class UserStatesViewModel : ReactiveObject
    {
        public UserStatesViewModel(IEnumerable<UserStateInfo> states)
        {
            States = new ObservableCollection<UserStateInfo>(states);
        }

        public ObservableCollection<UserStateInfo> States { get; }
    }
}
