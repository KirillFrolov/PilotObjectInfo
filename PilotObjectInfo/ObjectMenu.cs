using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Menu;
using Ascon.Pilot.SDK.Toolbar;
using PilotObjectInfo.ViewModels;
using PilotObjectInfo.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo
{
    [Export(typeof(IMenu<StorageContext>))]
    [Export(typeof(IMenu<ObjectsViewContext>))]
    [Export(typeof(IMenu<TasksViewContext2>))]
    //[Export(typeof(IToolbar<ObjectsViewContext>))]
    //[Export(typeof(IToolbar<TasksViewContext>))]
    public class ObjectMenu : IMenu<ObjectsViewContext>, IMenu<StorageContext>, IMenu<TasksViewContext2> //,
    // IToolbar<ObjectsViewContext>, IToolbar<TasksViewContext>

    {
        private readonly DialogService _dialogService;

        [ImportingConstructor]
        public ObjectMenu(IObjectsRepository objectsRepository, IFileProvider fileProvider,
            ITabServiceProvider tabServiceProvider, IObjectModifier objectModifier)
        {
            _dialogService = new DialogService(objectsRepository, fileProvider, tabServiceProvider,
                new FileModifier(objectModifier, objectsRepository),
                new AttributeModifier(objectModifier, objectsRepository));
        }

        public void Build(IMenuBuilder builder, StorageContext context)
        {
            AddItem(builder);
        }

        public void Build(IMenuBuilder builder, ObjectsViewContext context)
        {
            AddItem(builder);
        }

        public void Build(IMenuBuilder builder, TasksViewContext2 context)
        {
            AddItem(builder);
        }

        public void OnMenuItemClick(string name, StorageContext context)
        {
            if (context.SelectedObjects.Count() > 1 || !context.SelectedObjects.Any()) return;
            IDataObject obj = context.SelectedObjects.First().DataObject;
            _dialogService.ShowInfo(obj);
        }

        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {
            if (context.SelectedObjects.Count() > 1 || !context.SelectedObjects.Any()) return;
            IDataObject obj = context.SelectedObjects.First();
            _dialogService.ShowInfo(obj);
        }


        public void OnMenuItemClick(string name, TasksViewContext2 context)
        {
            if (context.SelectedTasks.Count() > 1 || !context.SelectedTasks.Any()) return;
            IDataObject obj = context.SelectedTasks.First();
            _dialogService.ShowInfo(obj);
        }


        private void AddItem(IMenuBuilder builder)
        {
            builder.AddItem("objectInfo", 0).WithHeader("Информация об объекте");
        }
    }
}