using Ascon.Pilot.SDK;
using PilotHelper.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo
{
    class FileModifier
    {
        private IObjectModifier _objectModifier;
        private IObjectsRepository _objectsRepository;

        public FileModifier( IObjectModifier objectModifier, IObjectsRepository objectsRepository)
        {
            
            _objectModifier = objectModifier;
            _objectsRepository = objectsRepository;
        }

        public async Task<IEnumerable<IFile>> AddFiles(Guid id, IEnumerable<string> filePaths)
        {
            if (filePaths == null) return null;
            if (filePaths.Count() == 0) return null;
            var builder = _objectModifier.EditById(id);
            foreach (string filePath in filePaths)
            {
                builder.AddFile(filePath);
            }
            _objectModifier.Apply();
            var obj = (await _objectsRepository.GetObjectsAsync(new Guid[] { id }, o => o, System.Threading.CancellationToken.None)).FirstOrDefault();
            return obj.ActualFileSnapshot.Files;
        }

        public async Task<IEnumerable<IFile>> RemoveFile(Guid id, IFile file)
        {
            var builder = _objectModifier.EditById(id);
            builder.RemoveFile(file.Id);
            _objectModifier.Apply();
            var obj = (await _objectsRepository.GetObjectsAsync(new Guid[] { id }, o => o, System.Threading.CancellationToken.None)).FirstOrDefault();
            return obj.ActualFileSnapshot.Files;
        }
    }
}
