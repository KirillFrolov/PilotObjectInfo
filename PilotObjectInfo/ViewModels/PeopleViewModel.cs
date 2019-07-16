using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class PeopleViewModel
    {
        

        public PeopleViewModel(IEnumerable<IPerson> people)
        {
            People = new ObservableCollection<IPerson>(people);
            
        }

       public ObservableCollection<IPerson> People { get; }
    }
}
