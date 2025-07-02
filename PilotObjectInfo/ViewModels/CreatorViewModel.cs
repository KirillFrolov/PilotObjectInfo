using Ascon.Pilot.SDK;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
	class CreatorViewModel : ReactiveObject
    {
		private IPerson _creator;

		public CreatorViewModel(IPerson creator)
		{
			_creator = creator;
		}

		public int? Id => _creator?.Id;
		public string ActualName => _creator?.ActualName;
		public string DisplayName => _creator?.DisplayName;
		public bool? IsAdmin => _creator?.IsAdmin;
		public string Login => _creator?.Login;
		public int? MainPosition => _creator?.MainPosition?.Position;
		public string Sid => _creator?.Sid;


	}
}
