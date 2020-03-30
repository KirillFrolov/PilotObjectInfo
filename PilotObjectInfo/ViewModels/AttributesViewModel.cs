using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.ViewModels
{
    class AttributesViewModel : ObservableObject
    {
        private IDictionary<string, object> _attributes;

        public AttributesViewModel(IDictionary<string, object> attributes)
        {
            _attributes = attributes;
            Attributes = _attributes.Select(x => new { x.Key, Value = x.Value?.ToString() }).ToDictionary(x => x.Key, y => y.Value);
        }

        public AttributesViewModel(IDataObject obj)
        {
            Attributes = new Dictionary<string, string>();

            foreach (var attr in obj.Attributes)
            {
                var attrType = obj.Type.Attributes.FirstOrDefault(x => x.Name.Equals(attr.Key));
                if (attrType == null)
                {
                    Attributes.Add(attr.Key, attr.Value?.ToString());
                    continue;
                }
                switch (attrType.Type)
                {
                    case AttributeType.Array:
                    case AttributeType.OrgUnit:
                        Attributes.Add(attr.Key, (ArrayToString<int>(attr.Value)));
                        break;
                    case AttributeType.Integer:
                    case AttributeType.Double:
                    case AttributeType.DateTime:
                    case AttributeType.String:
                    case AttributeType.Decimal:
                    case AttributeType.Numerator:
                    case AttributeType.UserState:
                    default:
                        Attributes.Add(attr.Key, attr.Value?.ToString());
                        break;
                }
            }
        }

        private string ArrayToString<T>(object value)
        {
            if (!(value is T[] arr)) return value?.ToString();
            return $"[{String.Join(",", arr)}]";
        }

        public Dictionary<string, string> Attributes { get; }

    }
}
