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
    public class ObjectMenu : IMenu<ObjectsViewContext>, IMenu<StorageContext>, IMenu<TasksViewContext2>//,
       // IToolbar<ObjectsViewContext>, IToolbar<TasksViewContext>

    {
        private IObjectsRepository _objectsRepository;
        private IObjectModifier _objectModifier;
        private IFileProvider _fileProvider;
        private ITabServiceProvider _tabServiceProvider;
        private FileModifier _fileModifier;

        [ImportingConstructor]
        public ObjectMenu(IObjectsRepository objectsRepository, IFileProvider fileProvider, ITabServiceProvider tabServiceProvider, IObjectModifier objectModifier)
        {
            _objectsRepository = objectsRepository;
            _objectModifier = objectModifier;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
            _fileModifier = new FileModifier(_objectModifier, _objectsRepository);
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
            if (context.SelectedObjects.Count() > 1 || context.SelectedObjects.Count() == 0) return;
            IDataObject obj = context.SelectedObjects.First().DataObject;
            DialogService.ShowInfo(obj, _objectsRepository, _fileProvider, _tabServiceProvider, _fileModifier);
        }

        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {
            if (context.SelectedObjects.Count() > 1 || context.SelectedObjects.Count() == 0) return;
            IDataObject obj = context.SelectedObjects.First();
            DialogService.ShowInfo(obj, _objectsRepository, _fileProvider, _tabServiceProvider, _fileModifier);
        }


        public void OnMenuItemClick(string name, TasksViewContext2 context)
        {
            if (context.SelectedTasks.Count() > 1 || context.SelectedTasks.Count() == 0) return;
            IDataObject obj = context.SelectedTasks.First();
            DialogService.ShowInfo(obj, _objectsRepository, _fileProvider, _tabServiceProvider, _fileModifier);
        }

        //public void OnToolbarItemClick(string name, ObjectsViewContext context)
        //{
        //    if (context.SelectedObjects.Count() > 1 || context.SelectedObjects.Count() == 0) return;
        //    IDataObject obj = context.SelectedObjects.First();
        //    DialogService.ShowInfo(obj, _objectsRepository, _fileProvider, _tabServiceProvider);
        //}

        //public void OnToolbarItemClick(string name, TasksViewContext context)
        //{
        //    if (context.SelectedTasks.Count() > 1 || context.SelectedTasks.Count() == 0) return;
        //    IDataObject obj = context.SelectedTasks.First().ToDataObject();
        //    DialogService.ShowInfo(obj, _objectsRepository, _fileProvider, _tabServiceProvider);
        //}

        private void AddItem(IMenuBuilder builder)
        {
            builder.AddItem("objectInfo", 0).WithHeader("Информация об объекте");
        }

        //private void AddItem(IToolbarBuilder builder)
        //{
        //    builder.AddButtonItem("objectInfo", 0).WithHeader("Информация об объекте");
        //}



    }
}
