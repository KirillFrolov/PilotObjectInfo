using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class OrgUnitViewModel
    {
        private IOrganisationUnit _organisationUnit;

        public OrgUnitViewModel(IEnumerable<IOrganisationUnit> organisationUnits, IOrganisationUnit organisationUnit)
        {
            _organisationUnit = organisationUnit;
            var children = organisationUnit.Children.Select(x => organisationUnits.FirstOrDefault(y => y.Id == x)).Where(x => x != null);
            Children = new ObservableCollection<OrgUnitViewModel>(children.Select(x=> new OrgUnitViewModel(organisationUnits, x)));

        }

        public int Id => _organisationUnit.Id;

        public string Title => _organisationUnit.Title;

        public bool IsDeleted => _organisationUnit.IsDeleted;

        public bool IsPosition => _organisationUnit.IsPosition;

        public bool IsChief => _organisationUnit.IsChief;


        public ObservableCollection<OrgUnitViewModel> Children { get; }
    }
}
