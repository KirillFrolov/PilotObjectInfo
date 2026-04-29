using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Threading;
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Data;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Models.Mapping;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class HistoryViewModel : ReactiveObject
    {
        private readonly DialogService _dialogService;
        private IDisposable _subscription;
        private ReactiveCommand<DataObject, Unit> _showInfoCmd;

        public HistoryViewModel(ReadOnlyCollection<Guid> historyItems, IObjectsRepository repository, DialogService dialogService)
        {
            _dialogService = dialogService;
            HistoryItems = new ObservableCollection<HistoryItem>();
            
            // Подписка на получение истории объекта
            _subscription = repository
                .GetHistoryItems(historyItems)
                .ObserveOnDispatcher(DispatcherPriority.Background)
                .Where(h => h.Object.State == DataState.Loaded)
                .Subscribe(
                    onNext: historyItem =>
                    {
                        var item = historyItem.ToHistoryItem();
                        if (item != null)
                        {
                            HistoryItems.Add(item);
                        }
                    },
                    onError: ex =>
                    {
                        // Обработка ошибок при загрузке истории
                        System.Diagnostics.Debug.WriteLine($"Error loading history: {ex.Message}");
                    },
                    onCompleted: () =>
                    {
                        // Отписка после получения всех данных
                        _subscription?.Dispose();
                        _subscription = null;
                    }
                );
        }

        public ObservableCollection<HistoryItem> HistoryItems { get; }

        public ReactiveCommand<DataObject, Unit> ShowInfoCmd
        {
            get
            {
                return _showInfoCmd ?? (_showInfoCmd = ReactiveCommand.Create<DataObject, Unit>(obj =>
                {
                    DoShowInfo(obj);
                    return Unit.Default;
                }));
            }
        }

        private void DoShowInfo(DataObject obj)
        {
            if (obj != null)
            {
                _dialogService.ShowInfo(obj);
            }
        }
    }
}

