using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Commands;
using Homebrew.Mvvm.Models;
using PilotHelper;
using PilotHelper.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class ChildrenViewModel: ObservableObject
    {
        private IEnumerable<Guid> _children;
        private IObjectsRepository _objectsRepository;
        private IFileProvider _fileProvider;
        private ITabServiceProvider _tabServiceProvider;
        private RelayCommand _showInfoCmd;

        public ChildrenViewModel(IEnumerable<Guid> children,IObjectsRepository objectsRepository, IFileProvider fileProvider, ITabServiceProvider tabServiceProvider)
        {
            _children = children;
            _objectsRepository = objectsRepository;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
        }

        public IEnumerable<Guid> Children => _children;

        public RelayCommand ShowInfoCmd
        {
            get
            {
                if (_showInfoCmd == null)
                {
                    _showInfoCmd = new RelayCommand(DoShowInfo);
                }
                return _showInfoCmd;
            }
        }

        private void DoShowInfo(object obj)
        {
            Guid id = (Guid)obj;
           DialogService.ShowInfo(id, _objectsRepository, _fileProvider, _tabServiceProvider);
        }
    }
}
