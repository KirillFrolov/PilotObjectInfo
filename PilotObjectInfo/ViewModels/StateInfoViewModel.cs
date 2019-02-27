using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class StateInfoViewModel : ObservableObject
    {
        private IStateInfo _stateInfo;

        public StateInfoViewModel(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }

        public string State => _stateInfo.State.ToString();

        public DateTime? Date => _stateInfo?.Date;

        public int? PersonId => _stateInfo?.PersonId;

        public int? PositionId => _stateInfo?.PositionId;


    }
}
