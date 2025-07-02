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
        private IEnumerable<Guid> _children;
        private IObjectsRepository _objectsRepository;
        private FileModifier _fileModifier;
        private IFileProvider _fileProvider;
        private ITabServiceProvider _tabServiceProvider;
        private ReactiveCommand<Guid, Unit> _showInfoCmd;

        public ChildrenViewModel(IEnumerable<Guid> children, IObjectsRepository objectsRepository, IFileProvider fileProvider, ITabServiceProvider tabServiceProvider, FileModifier fileModifier)
        {
            _children = children;
            _objectsRepository = objectsRepository;
            _fileModifier = fileModifier;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
        }

        public IEnumerable<Guid> Children => _children;


        public ReactiveCommand<Guid, Unit> ShowInfoCmd
        {
            get
            {
                if (_showInfoCmd == null)
                {
                    _showInfoCmd = ReactiveCommand.Create<Guid, Unit>(o =>
                    {
                        DoShowInfo(o);
                        return Unit.Default;
                    });
                }
                return _showInfoCmd;
            }
        }

        private void DoShowInfo(Guid obj)
        {
            var id = obj;
            DialogService.ShowInfo(id, _objectsRepository, _fileProvider, _tabServiceProvider, _fileModifier);
        }
    }
}
