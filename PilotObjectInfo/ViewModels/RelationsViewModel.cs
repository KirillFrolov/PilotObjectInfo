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
	class RelationsViewModel : ObservableObject
    {
		private ReadOnlyCollection<IRelation> _relations;

		public RelationsViewModel(ReadOnlyCollection<IRelation> relations)
		{
			_relations = relations;
		}

		public ReadOnlyCollection<IRelation> Relations => _relations;
	}
}
