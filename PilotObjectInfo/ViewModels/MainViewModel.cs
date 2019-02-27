using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
	class MainViewModel : ObservableObject
    {
		private IDataObject _obj;
        private IFileProvider _fileProvider;
        private IObjectsRepository _objectsRepository;

        public MainViewModel(IDataObject obj, IObjectsRepository objectsRepository, IFileProvider fileProvider)
		{
			_obj = obj;
            _fileProvider = fileProvider;
            _objectsRepository = objectsRepository;

            AttributesVm = new AttributesViewModel(_obj.Attributes);
			TypeVm = new TypeViewModel(_obj.Type);
			CreatorVm = new CreatorViewModel(_obj.Creator);
			FilesVm = new FilesViewModel(_obj.Files, _fileProvider);
			AccessVm = new AccessViewModel(_obj.Access2);
			RelationsVm = new RelationsViewModel(obj.Relations);
            StateInfoVm = new StateInfoViewModel(obj.ObjectStateInfo);
            ChildrenVm = new ChildrenViewModel(obj.Children, _objectsRepository, _fileProvider);
        }

		public Guid Id => _obj.Id;

		public string DisplayName => _obj.DisplayName;

		public DateTime Created => _obj.Created;

		public bool IsSecret => _obj.IsSecret;

		public Guid ParentId => _obj.ParentId;

		public AttributesViewModel AttributesVm { get; }
		public TypeViewModel TypeVm { get; }
		public CreatorViewModel CreatorVm { get; }
		public FilesViewModel FilesVm { get; }
		public AccessViewModel AccessVm { get; }
		public RelationsViewModel RelationsVm { get; }
        public StateInfoViewModel StateInfoVm { get; }
        public ChildrenViewModel ChildrenVm { get;}
    }
}
