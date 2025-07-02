using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using PilotObjectInfo.Extensions;

namespace PilotObjectInfo
{
    public class AttributeModifier
    {
        private readonly IObjectModifier _objectModifier;
        private readonly IObjectsRepository _objectsRepository;

        public AttributeModifier(IObjectModifier objectModifier, IObjectsRepository objectsRepository)
        {
            _objectModifier = objectModifier;
            _objectsRepository = objectsRepository;
        }
        
        public async Task<IDictionary<string, object>> DeleteAttributeAsync(Guid id, string name)
        {
            var builder = _objectModifier.EditById(id);
            builder.RemoveAttribute(name);
            _objectModifier.Apply();
            var obj = await _objectsRepository.GetObjectAsync(id);
            return obj.Attributes;
        }
    }
}