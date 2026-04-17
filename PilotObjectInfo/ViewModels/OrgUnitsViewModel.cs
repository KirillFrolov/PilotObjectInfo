using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Models.Support;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class OrgUnitsViewModel: ReactiveObject
    {
        public OrgUnitsViewModel(List<OrganisationUnit> organisationUnits)
        {
            var rootUnits = organisationUnits.Where(x => x.Id == 0).ToList();
            OrganisationUnits = new ObservableCollection<OrgUnitViewModel>(
                rootUnits.Select(x => new OrgUnitViewModel(organisationUnits, x)));
        }

        public ObservableCollection<OrgUnitViewModel> OrganisationUnits { get; }


    }
}
