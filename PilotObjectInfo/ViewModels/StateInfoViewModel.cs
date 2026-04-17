using System;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class StateInfoViewModel : ReactiveObject
    {
        private StateInfo _stateInfo;

        public StateInfoViewModel(StateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }

        public string State => _stateInfo?.State.ToString();

        public DateTime? Date => _stateInfo?.Date;

        public int? PersonId => _stateInfo?.PersonId;

        public int? PositionId => _stateInfo?.PositionId;


    }
}
