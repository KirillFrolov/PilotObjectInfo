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
    class TypesViewModel: ObservableObject
    {
        public TypesViewModel(IEnumerable<IType> types)
        {
            Types = new ObservableCollection<IType>(types);
        }

        public ObservableCollection<IType> Types { get; }
    }
}
