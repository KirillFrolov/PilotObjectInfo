using Ascon.Pilot.SDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class TypesViewModel: ReactiveObject
    {
        public TypesViewModel(IEnumerable<IType> types)
        {
            Types = new ObservableCollection<IType>(types);
        }

        public ObservableCollection<IType> Types { get; }
    }
}
