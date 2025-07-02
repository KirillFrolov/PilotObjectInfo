using System.Collections.ObjectModel;
using Ascon.Pilot.SDK;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class AccessViewModel : ReactiveObject 
    {
        private ReadOnlyCollection<IAccessRecord> _access2;

        public AccessViewModel(ReadOnlyCollection<IAccessRecord> access2)
        {
            _access2 = access2;
        }

        public ReadOnlyCollection<IAccessRecord> Access => _access2;
    }
}