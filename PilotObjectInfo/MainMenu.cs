using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Menu;
using PilotHelper.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PilotObjectInfo
{

    [Export(typeof(IMenu<MainViewContext>))]
    public class MainMenu : IMenu<MainViewContext>
    {
        
        IObjectsRepository _objectsRepository;
        private IFileProvider _fileProvider;
        private ITabServiceProvider _tabServiceProvider;
        private const string MY_SUB_MENU = "ShowObjectInfo";
        private const string GUID_PATTERN = @"([0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12})";

        [ImportingConstructor]
        public MainMenu(IObjectsRepository objectsRepository, IFileProvider fileProvider, ITabServiceProvider tabServiceProvider)
        {
            _objectsRepository = objectsRepository;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
        }

        public void Build(IMenuBuilder builder, MainViewContext context)
        {
            var item = builder.AddItem("ObjectInfo", 1).WithHeader("Object Info");
            item.WithSubmenu().AddItem(MY_SUB_MENU, 0).WithHeader("Show");
        }

        public void OnMenuItemClick(string name, MainViewContext context)
        {
            if (name != MY_SUB_MENU) return;
            Regex rgx = new Regex(GUID_PATTERN, RegexOptions.IgnoreCase);
            var clipboardText = Clipboard.GetText(TextDataFormat.Text);
            var match = rgx.Match(clipboardText);
            if (match.Success == false) return;
            var id = new Guid(match.Groups[1].Value);
            //var obj = (await _objectsRepository.GetObjectsAsync(new Guid[] { id }, o => o, System.Threading.CancellationToken.None)).FirstOrDefault();
            //if (obj == null) return;
            DialogService.ShowInfo(id, _objectsRepository, _fileProvider, _tabServiceProvider);

        }
    }
}
