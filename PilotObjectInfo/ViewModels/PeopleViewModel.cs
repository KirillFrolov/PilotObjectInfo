using Ascon.Pilot.SDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class PeopleViewModel: ReactiveObject
    {
        

        public PeopleViewModel(IEnumerable<IPerson> people)
        {
            People = new ObservableCollection<IPerson>(people);
            
        }

       public ObservableCollection<IPerson> People { get; }
    }
}
