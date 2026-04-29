using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Services;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class RelationsViewModel : ReactiveObject
    {
        private readonly List<RelationInfo> _relations;
        private readonly DialogService _dialogService;
        private readonly NavigationService _navigationService;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;
        private ReactiveCommand<Guid, Unit> _goToCmd;

        public RelationsViewModel(List<RelationInfo> relations,
            DialogService dialogService,
            NavigationService navigationService)
        {
            _relations = relations;
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public List<RelationInfo> Relations => _relations;

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

        public ReactiveCommand<Guid, Unit> GoToCmd
        {
            get
            {
                return _goToCmd ?? (_goToCmd = ReactiveCommand.Create<Guid, Unit>(id =>
                {
                    DoGoTo(id);
                    return Unit.Default;
                }));
            }
        }

        private async Task DoShowInfo(Guid id)
        {
            await _dialogService.ShowInfoAsync(id);
        }

        private void DoGoTo(Guid id)
        {
            if (id == Guid.Empty)
                return;
            
            _navigationService.ShowElement(id);
        }
    }
}