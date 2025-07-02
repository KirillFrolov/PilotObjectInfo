using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class AttributesViewModel : ReactiveObject
    {
        private readonly AttributeModifier _attributeModifier;

        private ReactiveCommand<KeyValuePair<string, string>, Unit> _deleteAttributeCmd;
        private readonly Guid _id;
        private readonly IType _type;



        public AttributesViewModel(IDataObject obj, AttributeModifier attributeModifier)
        {
            _id = obj.Id;
            _type = obj.Type;
            _attributeModifier = attributeModifier;
            Attributes = new ObservableCollection<KeyValuePair<string, string>>();
            InitAttributes(obj.Attributes);
        }

        private void InitAttributes(IDictionary<string, object> attributes)
        {
            Attributes.Clear();
            foreach (var attr in attributes)
            {
                var attrType = _type.Attributes.FirstOrDefault(x => x.Name.Equals(attr.Key));
                if (attrType == null)
                {
                    Attributes.Add(new KeyValuePair<string, string>(attr.Key, attr.Value?.ToString()));
                    continue;
                }

                switch (attrType.Type)
                {
                    case AttributeType.Array:
                    case AttributeType.OrgUnit:
                        Attributes.Add(new KeyValuePair<string, string>(attr.Key, (ArrayToString<int>(attr.Value))));
                        break;
                    case AttributeType.Integer:
                    case AttributeType.Double:
                    case AttributeType.DateTime:
                    case AttributeType.String:
                    case AttributeType.Decimal:
                    case AttributeType.Numerator:
                    case AttributeType.UserState:
                    default:
                        Attributes.Add(new KeyValuePair<string, string>(attr.Key, attr.Value?.ToString()));
                        break;
                }
            }
        }

        private string ArrayToString<T>(object value)
        {
            if (!(value is T[] arr)) return value?.ToString();
            return $"[{String.Join(",", arr)}]";
        }

        public ObservableCollection<KeyValuePair<string, string>> Attributes { get; }


        public ReactiveCommand<KeyValuePair<string, string>, Unit> DeleteAttributeCmd
        {
            get
            {
                return _deleteAttributeCmd ?? (_deleteAttributeCmd =
                    ReactiveCommand.CreateFromTask<KeyValuePair<string, string>, Unit>(async o =>
                    {
                        await DeleteAttribute(o);
                        return Unit.Default;
                    }));
            }
        }

        private async Task DeleteAttribute(KeyValuePair<string, string> attribute)
        {
            if (string.IsNullOrEmpty(attribute.Key)) return;
            if (_attributeModifier != null)
            {
                var newAttributes = await _attributeModifier.DeleteAttributeAsync(_id, attribute.Key);
                InitAttributes(newAttributes);
            }
        }
    }
}