using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Ascon.Pilot.SDK;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class RelationsViewModel : ReactiveObject
    {
        private readonly ReadOnlyCollection<IRelation> _relations;
        private readonly IObjectsRepository _objectsRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ITabServiceProvider _tabServiceProvider;
        private readonly FileModifier _fileModifier;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;

        public RelationsViewModel(ReadOnlyCollection<IRelation> relations,
            IObjectsRepository objectsRepository,
            IFileProvider fileProvider,
            ITabServiceProvider tabServiceProvider,
            FileModifier fileModifier)
        {
            _relations = relations;
            _objectsRepository = objectsRepository;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
            _fileModifier = fileModifier;
        }

        public ReadOnlyCollection<IRelation> Relations => _relations;

        public ReactiveCommand<Guid, Unit> ShowInfoCmd
        {
            get
            {
                return _showInfoCmd ?? (_showInfoCmd = ReactiveCommand.Create<Guid, Unit>(id =>
                {
                    DoShowInfo(id);
                    return Unit.Default;
                }));
            }
        }

        private void DoShowInfo(Guid id)
        {
            DialogService.ShowInfo(id, _objectsRepository, _fileProvider, _tabServiceProvider, _fileModifier);
        }
    }
}