using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class HistoryViewModel : ReactiveObject
    {
        private readonly DialogService _dialogService;

        public HistoryViewModel(DialogService dialogService)
        {
            _dialogService = dialogService;
            
            
        }
    }
}