using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class RelationsViewModel : ReactiveObject
    {
        private readonly List<RelationInfo> _relations;
        private readonly DialogService _dialogService;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;

        public RelationsViewModel(List<RelationInfo> relations,
            DialogService dialogService)
        {
            _relations = relations;
            _dialogService = dialogService;
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

        private async Task DoShowInfo(Guid id)
        {
            await _dialogService.ShowInfoAsync(id);
        }
    }
}