using Ascon.Pilot.SDK;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
	class TypeViewModel : ReactiveObject
    {
		private IType _type;

		public TypeViewModel(IType type)
		{
			_type = type;
		}
		public int Id => _type.Id;
		public string Name => _type.Name;
		public string Title => _type.Title;
		public string Kind => _type.Kind.ToString();
		public bool IsMountable => _type.IsMountable;
		public bool IsProject => _type.IsProject;
		public bool IsService => _type.IsService;
		
	}
}
