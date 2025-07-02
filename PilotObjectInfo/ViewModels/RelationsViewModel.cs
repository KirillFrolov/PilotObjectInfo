using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
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
        private readonly DialogService _dialogService;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;

        public RelationsViewModel(ReadOnlyCollection<IRelation> relations,
            IObjectsRepository objectsRepository,
            IFileProvider fileProvider,
            ITabServiceProvider tabServiceProvider,
            FileModifier fileModifier,
            DialogService dialogService)
        {
            _relations = relations;
            _objectsRepository = objectsRepository;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
            _fileModifier = fileModifier;
            _dialogService = dialogService;
        }

        public ReadOnlyCollection<IRelation> Relations => _relations;

        public ReactiveCommand<Guid, Unit> ShowInfoCmd
        {
            get
            {
                return _showInfoCmd ?? (_showInfoCmd = ReactiveCommand.CreateFromTask<Guid, Unit>(async id =>
                {
                    await DoShowInfo(id);
                    return Unit.Default;
                }));
            }
        }

        private async Task DoShowInfo(Guid id)
        {
            await _dialogService.ShowInfoAsync(id);
        }
    }
}