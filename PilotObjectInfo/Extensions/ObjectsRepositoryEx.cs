using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Extensions
{
    public static class ObjectsRepositoryEx
    {
        private static IObservable<IList<IDataObject>> GetObservableList(
            IObjectsRepository repository,
            IEnumerable<Guid> ids,
            CancellationToken ct)
        {
            var cancel = Observable.Create<IDataObject>(o => ct.Register(o.OnCompleted));

            var loading = ids.ToList();
            return repository
                .SubscribeObjects(loading)
                .TakeUntil(cancel)
                .ObserveOnDispatcher(DispatcherPriority.Background)
                .Where(o => o.State == DataState.Loaded)
                .Distinct(o => o.Id)
                .Take(loading.Count)
                .ToList();
        }


        private static IObservable<IList<IDataObject>> GetObservableList(
            IObjectsRepository repository,
            IEnumerable<Guid> ids)
        {
            var loading = ids.ToList();
            return repository
                .SubscribeObjects(loading)
                .ObserveOnDispatcher(DispatcherPriority.Background)
                .Where(o => o.State == DataState.Loaded)
                .Distinct(o => o.Id)
                .Take(loading.Count)
                .ToList();
        }

        public static Task<IEnumerable<IDataObject>> GetObjectsAsync(
            this IObjectsRepository repository,
            IEnumerable<Guid> ids,
            CancellationToken ct)
        {
            var observableList = GetObservableList(repository, ids, ct);
            return Task<IEnumerable<IDataObject>>.Factory.StartNew(() =>
            {
                var lazy = observableList.Wait();
                return lazy.ToList();
            }, ct);
        }

        public static Task<IEnumerable<IDataObject>> GetObjectsAsync(
            this IObjectsRepository repository,
            IEnumerable<Guid> ids
        )
        {
            var observableList = GetObservableList(repository, ids);
            return Task<IEnumerable<IDataObject>>.Factory.StartNew(() =>
            {
                var lazy = observableList.Wait();
                return lazy.ToList();
            });
        }

        public static Task<IDataObject> GetObjectAsync(
            this IObjectsRepository repository,
            Guid id,
            CancellationToken ct)
        {
            var loading = new[] { id };
            var observableList = GetObservableList(repository, loading, ct);

            return Task<IDataObject>.Factory.StartNew(() =>
            {
                var lazy = observableList.Wait();
                return lazy.FirstOrDefault();
            }, ct);
        }

        public static Task<IDataObject> GetObjectAsync(this IObjectsRepository repository, Guid id)
        {
            var loading = new[] { id };
            var observableList = GetObservableList(repository, loading);

            return Task<IDataObject>.Factory.StartNew(() =>
            {
                var lazy = observableList.Wait();
                return lazy.FirstOrDefault();
            });
        }
    }
}