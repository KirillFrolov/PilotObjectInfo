using System.Collections.Generic;
using System.Collections.ObjectModel;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class TypesViewModel: ReactiveObject
    {
        public TypesViewModel(IEnumerable<TypeInfo> types)
        {
            Types = new ObservableCollection<TypeInfo>(types);
        }

        public ObservableCollection<TypeInfo> Types { get; }
    }
}
