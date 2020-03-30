using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Commands;
using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private IDataObject _obj;
        private IFileProvider _fileProvider;
        private IObjectsRepository _objectsRepository;
        private ITabServiceProvider _tabServiceProvider;
        private RelayCommand _goToCommand;

        public MainViewModel(IDataObject obj, IObjectsRepository objectsRepository, FileModifier fileModifier, IFileProvider fileProvider, ITabServiceProvider tabServiceProvider)
        {
            _obj = obj;
            _fileProvider = fileProvider;
            _objectsRepository = objectsRepository;
            _tabServiceProvider = tabServiceProvider;
            
            AttributesVm = new AttributesViewModel(_obj);
            TypeVm = new TypeViewModel(_obj.Type);
            CreatorVm = new CreatorViewModel(_obj.Creator);
            FilesVm = new FilesViewModel(obj.Id, _obj.Files, _fileProvider, fileModifier);
            SnapshotsVm = new SnapshotsViewModel(_obj.Id, _obj.PreviousFileSnapshots, _fileProvider);

            AccessVm = new AccessViewModel(_obj.Access2);
            RelationsVm = new RelationsViewModel(obj.Relations);
            StateInfoVm = new StateInfoViewModel(obj.ObjectStateInfo);
            ChildrenVm = new ChildrenViewModel(obj.Children, _objectsRepository, _fileProvider, _tabServiceProvider, fileModifier);
            PeopleVm = new PeopleViewModel(_objectsRepository.GetPeople());
            OrgUnitsVm = new OrgUnitsViewModel(_objectsRepository.GetOrganisationUnits());
            TypesVm = new TypesViewModel(_objectsRepository.GetTypes());
            UserStatesVm = new UserStatesViewModel( _objectsRepository.GetUserStates());

            _objectsRepository.GetOrganisationUnits();
        }

        public Guid Id => _obj.Id;

        public string DisplayName => _obj.DisplayName;

        public DateTime Created => _obj.Created;

        public bool IsSecret => _obj.IsSecret;

        public Guid ParentId => _obj.ParentId;

        public int CurrentUserId => _objectsRepository.GetCurrentPerson().Id;

        public AttributesViewModel AttributesVm { get; }
        public TypeViewModel TypeVm { get; }
        public CreatorViewModel CreatorVm { get; }
        public FilesViewModel FilesVm { get; }
        public SnapshotsViewModel SnapshotsVm { get; }
        public AccessViewModel AccessVm { get; }
        public RelationsViewModel RelationsVm { get; }
        public StateInfoViewModel StateInfoVm { get; }
        public ChildrenViewModel ChildrenVm { get; }
        public PeopleViewModel PeopleVm { get; }
        public OrgUnitsViewModel OrgUnitsVm { get; }
        public UserStatesViewModel UserStatesVm { get; }

        public TypesViewModel TypesVm { get; }

        public RelayCommand GoToCommand
        {
            get
            {
                if (_goToCommand == null)
                {
                    _goToCommand = new RelayCommand(DoGoTo);
                }
                return _goToCommand;

            }
        }

        private void DoGoTo(object obj)
        {
            _tabServiceProvider.ShowElement(Id);
        }
    }
}
