using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class UserStatesViewModel : ObservableObject
    {
        public UserStatesViewModel(IEnumerable<IUserState> states)
        {
            States = new ObservableCollection<IUserState>(states);
        }

        public ObservableCollection<IUserState> States { get; }
    }
}
