using Ascon.Pilot.SDK;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class OrgUnitsViewModel: ReactiveObject
    {
        public OrgUnitsViewModel(IEnumerable<IOrganisationUnit> organisationUnits)
        {
            OrganisationUnits = new ObservableCollection<OrgUnitViewModel>(organisationUnits.Where(x=>x.Id == 0).Select(x=> new OrgUnitViewModel(organisationUnits, x)));
        }

        public ObservableCollection<OrgUnitViewModel> OrganisationUnits { get; }


    }
}
