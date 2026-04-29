using System;
using System.Reactive;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Services;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class MainViewModel : ReactiveObject
    {
        private FileService _fileService;
        private readonly DataService _dataService;
        private readonly NavigationService _navigationService;
        private ReactiveCommand<Unit, Unit> _goToCommand;

        public MainViewModel(DataObject obj, DataService dataService, FileModifier fileModifier,
            FileService fileService, NavigationService navigationService,
            AttributeModifier attributeModifier, DialogService dialogService)
        {
            _fileService = fileService;
            _dataService = dataService;
            _navigationService = navigationService;

            AttributesVm = new AttributesViewModel(obj.Id, obj.Attributes, obj.Type, attributeModifier);
            TypeVm = new TypeViewModel(obj.Type);
            CreatorVm = new CreatorViewModel(obj.Creator);
            FilesVm = new FilesViewModel(obj.Id, obj.Files, _fileService, fileModifier);
            SnapshotsVm = new SnapshotsViewModel(obj.Id, obj.PreviousFileSnapshots, _fileService);

            AccessVm = new AccessViewModel(obj.Access2);
            RelationsVm = new RelationsViewModel(obj.Relations, dialogService);
            StateInfoVm = new StateInfoViewModel(obj.ObjectStateInfo);
            ChildrenVm = new ChildrenViewModel(obj.Children, dialogService);
            PeopleVm = new PeopleViewModel(_dataService.GetPeople());
            OrgUnitsVm = new OrgUnitsViewModel(_dataService.GetOrganisationUnits());
            TypesVm = new TypesViewModel(_dataService.GetTypes());
            UserStatesVm = new UserStatesViewModel(_dataService.GetUserStates());
            HistoryVm = new HistoryViewModel(obj.HistoryItems, _dataService.GetRepository(), dialogService);

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

        public int CurrentUserId => _dataService.GetCurrentPerson().Id ?? 0;

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
        
        public HistoryViewModel HistoryVm { get; }

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
            _navigationService.ShowElement(Id);
        }
    }
}