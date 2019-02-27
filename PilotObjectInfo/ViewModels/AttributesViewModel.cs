using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
	class AttributesViewModel : ObservableObject
    {
		private IDictionary<string, object> _attributes;

		public AttributesViewModel(IDictionary<string, object> attributes)
		{
			_attributes = attributes;
			Attributes = _attributes.Select(x => new { Key = x.Key, Value = x.Value?.ToString() }).ToDictionary(x => x.Key, y => y.Value);
		}

		public Dictionary<string, string> Attributes { get; }

	}
}
