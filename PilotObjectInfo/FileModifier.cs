using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PilotObjectInfo.Extensions;

namespace PilotObjectInfo
{
    class FileModifier
    {
        private readonly IObjectModifier _objectModifier;
        private readonly IObjectsRepository _objectsRepository;

        public FileModifier(IObjectModifier objectModifier, IObjectsRepository objectsRepository)
        {
            _objectModifier = objectModifier;
            _objectsRepository = objectsRepository;
        }

        public async Task<IEnumerable<IFile>> AddFiles(Guid id, IEnumerable<string> filePaths)
        {
            if (filePaths == null) return null;
            if (!filePaths.Any()) return null;
            var builder = _objectModifier.EditById(id);
            foreach (string filePath in filePaths)
            {
                builder.AddFile(filePath);
            }

            _objectModifier.Apply();
            var obj = await _objectsRepository.GetObjectAsync(id);
            return obj.ActualFileSnapshot.Files;
        }

        public async Task<IEnumerable<IFile>> RemoveFile(Guid id, IFile file)
        {
            var builder = _objectModifier.EditById(id);
            builder.RemoveFile(file.Id);
            _objectModifier.Apply();
            var obj = await _objectsRepository.GetObjectAsync(id);
            return obj.ActualFileSnapshot.Files;
        }
    }
}