using Ascon.Pilot.SDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class UserStatesViewModel : ReactiveObject
    {
        public UserStatesViewModel(IEnumerable<IUserState> states)
        {
            States = new ObservableCollection<IUserState>(states);
        }

        public ObservableCollection<IUserState> States { get; }
    }
}
