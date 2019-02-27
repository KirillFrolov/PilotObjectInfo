using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;

namespace PilotObjectInfo.ViewModels
{
	class AccessViewModel:ObservableObject
	{
		private ReadOnlyCollection<IAccessRecord> _access2;

		public AccessViewModel(ReadOnlyCollection<IAccessRecord> access2)
		{
			_access2 = access2;
		}

		public ReadOnlyCollection<IAccessRecord> Access => _access2;
	}
}
