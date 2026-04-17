using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using PilotObjectInfo.Models.Core;
using PilotObjectInfo.Models.Support;
using PilotObjectInfo.Models.Extensions;
using PilotObjectInfo.Extensions;

namespace PilotObjectInfo.Services
{
    /// <summary>
    /// Service wrapper for IObjectsRepository
    /// Encapsulates data access operations from SDK
    /// </summary>
    public class DataService
    {
        private readonly IObjectsRepository _objectsRepository;

        public DataService(IObjectsRepository objectsRepository)
        {
            _objectsRepository = objectsRepository;
        }

        public async Task<DataObject> GetObjectAsync(Guid id)
        {
            var sdkObject = await _objectsRepository.GetObjectAsync(id);
            return sdkObject.ToModel();
        }

        public List<Person> GetPeople()
        {
            return _objectsRepository.GetPeople().ToModels();
        }

        public List<OrganisationUnit> GetOrganisationUnits()
        {
            return _objectsRepository.GetOrganisationUnits().ToModels();
        }

        public List<TypeInfo> GetTypes()
        {
            return _objectsRepository.GetTypes().ToModels();
        }

        public List<UserStateInfo> GetUserStates()
        {
            return _objectsRepository.GetUserStates().ToModels();
        }

        public Person GetCurrentPerson()
        {
            return _objectsRepository.GetCurrentPerson().ToModel();
        }
    }
}

