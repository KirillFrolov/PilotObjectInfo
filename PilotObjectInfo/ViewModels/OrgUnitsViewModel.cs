using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class OrgUnitsViewModel
    {
        public OrgUnitsViewModel(IEnumerable<IOrganisationUnit> organisationUnits)
        {
            OrganisationUnits = new ObservableCollection<IOrganisationUnit>(organisationUnits);
        }

        public ObservableCollection<IOrganisationUnit> OrganisationUnits { get; }
    }
}
