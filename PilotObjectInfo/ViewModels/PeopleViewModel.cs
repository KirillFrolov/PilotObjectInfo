using System.Collections.Generic;
using System.Collections.ObjectModel;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class PeopleViewModel: ReactiveObject
    {
        

        public PeopleViewModel(IEnumerable<Person> people)
        {
            People = new ObservableCollection<Person>(people);
            
        }

       public ObservableCollection<Person> People { get; }
    }
}
