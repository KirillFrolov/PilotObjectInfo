using Ascon.Pilot.SDK;
using System;
using System.Reactive;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class MainViewModel : ReactiveObject
    {
        private IFileProvider _fileProvider;
        private readonly IObjectsRepository _objectsRepository;
        private readonly ITabServiceProvider _tabServiceProvider;
        private ReactiveCommand<Unit, Unit> _goToCommand;

        public MainViewModel(IDataObject obj, IObjectsRepository objectsRepository, FileModifier fileModifier,
            IFileProvider fileProvider, ITabServiceProvider tabServiceProvider,
            AttributeModifier attributeModifier, DialogService dialogService)
        {
            _fileProvider = fileProvider;
            _objectsRepository = objectsRepository;
            _tabServiceProvider = tabServiceProvider;

            AttributesVm = new AttributesViewModel(obj, attributeModifier);
            TypeVm = new TypeViewModel(obj.Type);
            CreatorVm = new CreatorViewModel(obj.Creator);
            FilesVm = new FilesViewModel(obj.Id, obj.Files, _fileProvider, fileModifier);
            SnapshotsVm = new SnapshotsViewModel(obj.Id, obj.PreviousFileSnapshots, _fileProvider);

            AccessVm = new AccessViewModel(obj.Access2);
            RelationsVm = new RelationsViewModel(obj.Relations, _objectsRepository, _fileProvider, _tabServiceProvider,
                fileModifier, dialogService);
            StateInfoVm = new StateInfoViewModel(obj.ObjectStateInfo);
            ChildrenVm = new ChildrenViewModel(obj.Children, dialogService);
            PeopleVm = new PeopleViewModel(_objectsRepository.GetPeople());
            OrgUnitsVm = new OrgUnitsViewModel(_objectsRepository.GetOrganisationUnits());
            TypesVm = new TypesViewModel(_objectsRepository.GetTypes());
            UserStatesVm = new UserStatesViewModel(_objectsRepository.GetUserStates());

            _objectsRepository.GetOrganisationUnits();
            Id = obj.Id;
            DisplayName = obj.DisplayName;
            Created = obj.Created;
            IsSecret = obj.IsSecret;
            ParentId = obj.ParentId;
        }

        public Guid Id { get; }


        public string DisplayName { get; }

        public DateTime Created { get; }

        public bool IsSecret { get; }

        public Guid ParentId { get; }

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

        public ReactiveCommand<Unit, Unit> GoToCommand
        {
            get
            {
                return _goToCommand ?? (_goToCommand = ReactiveCommand.Create<Unit, Unit>(_ =>
                {
                    DoGoTo();
                    return Unit.Default;
                }));
            }
        }

        private void DoGoTo()
        {
            _tabServiceProvider.ShowElement(Id);
        }
    }
}