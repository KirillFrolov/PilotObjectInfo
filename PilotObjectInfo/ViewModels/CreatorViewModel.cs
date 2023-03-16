using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;

namespace PilotObjectInfo.ViewModels
{
	class CreatorViewModel : ObservableObject
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
