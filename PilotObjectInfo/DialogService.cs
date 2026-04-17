using Ascon.Pilot.SDK;
using PilotObjectInfo.Extensions;
using PilotObjectInfo.Models.Extensions;
using PilotObjectInfo.Services;
using PilotObjectInfo.ViewModels;
using PilotObjectInfo.Views;
using System;
using System.Threading.Tasks;

namespace PilotObjectInfo
{
    class DialogService
    {
        private readonly IObjectsRepository _objectsRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ITabServiceProvider _tabServiceProvider;
        private readonly FileModifier _fileModifier;
        private readonly AttributeModifier _attributeModifier;

        public DialogService(IObjectsRepository objectsRepository, IFileProvider fileProvider,
            ITabServiceProvider tabServiceProvider, FileModifier fileModifier, AttributeModifier attributeModifier)
        {
            _objectsRepository = objectsRepository;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
            _fileModifier = fileModifier;
            _attributeModifier = attributeModifier;
        }

        public void ShowInfo(IDataObject obj)
        {
            if (obj == null) return;
            
            // Convert IDataObject to DataObject model
            var dataObject = obj.ToModel();
            
            // Create service wrappers
            var dataService = new DataService(_objectsRepository);
            var fileService = new FileService(_fileProvider);
            var navigationService = new NavigationService(_tabServiceProvider);
            
            var vm = new MainViewModel(dataObject, dataService, _fileModifier, fileService, navigationService,
                _attributeModifier, this);
            var v = new MainView() { DataContext = vm };
            v.Show();
        }

        public async Task ShowInfoAsync(Guid id)
        {
            var obj = await _objectsRepository.GetObjectAsync(id);
            ShowInfo(obj);
        }
    }
}