using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PilotObjectInfo.Models.Support;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class OrgUnitViewModel: ReactiveObject
    {
        private OrganisationUnit _organisationUnit;

        public OrgUnitViewModel(List<OrganisationUnit> organisationUnits, OrganisationUnit organisationUnit)
        {
            _organisationUnit = organisationUnit;
            var children = organisationUnit.Children
                .Select(childId => organisationUnits.FirstOrDefault(y => y.Id == childId))
                .Where(x => x != null);
            Children = new ObservableCollection<OrgUnitViewModel>(
                children.Select(x => new OrgUnitViewModel(organisationUnits, x)));
        }

        public int Id => _organisationUnit.Id;

        public string Title => _organisationUnit.Title;

        public bool IsDeleted => _organisationUnit.IsDeleted;

        public bool IsPosition => _organisationUnit.IsPosition;

        public bool IsChief => _organisationUnit.IsChief;


        public ObservableCollection<OrgUnitViewModel> Children { get; }
    }
}
