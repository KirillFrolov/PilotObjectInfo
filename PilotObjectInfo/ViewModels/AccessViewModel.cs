using System.Collections.Generic;
using System.Collections.ObjectModel;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class AccessViewModel : ReactiveObject 
    {
        private ReadOnlyCollection<AccessRecord> _access2;

        public AccessViewModel(List<AccessRecord> access2)
        {
            _access2 = new ReadOnlyCollection<AccessRecord>(access2 ?? new List<AccessRecord>());
        }

        public ReadOnlyCollection<AccessRecord> Access => _access2;
    }
}