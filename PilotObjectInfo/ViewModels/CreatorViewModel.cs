using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
	class CreatorViewModel : ReactiveObject
    {
		private Person _creator;

		public CreatorViewModel(Person creator)
		{
			_creator = creator;
		}

		public int? Id => _creator?.Id;
		public string ActualName => _creator?.ActualName;
		public string DisplayName => _creator?.DisplayName;
		public bool? IsAdmin => _creator?.IsAdmin;
		public string Login => _creator?.Login;
		public int? MainPosition => _creator?.MainPosition?.PositionId;
		public string Sid => _creator?.Sid;


	}
}
