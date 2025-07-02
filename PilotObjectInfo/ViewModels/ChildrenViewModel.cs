using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class ChildrenViewModel : ReactiveObject
    {
        private readonly IEnumerable<Guid> _children;
        private readonly DialogService _dialogService;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;

        public ChildrenViewModel(IEnumerable<Guid> children, 
            DialogService dialogService)
        {
            _children = children;
            _dialogService = dialogService;
        }

        public IEnumerable<Guid> Children => _children;


        public ReactiveCommand<Guid, Unit> ShowInfoCmd
        {
            get
            {
                return _showInfoCmd ?? (_showInfoCmd = ReactiveCommand.CreateFromTask<Guid, Unit>(async o =>
                {
                    await DoShowInfo(o);
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