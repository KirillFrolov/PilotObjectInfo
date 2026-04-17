using System;
using System.IO;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Services
{
    /// <summary>
    /// Service wrapper for IFileProvider
    /// Encapsulates file operations from SDK
    /// </summary>
    public class FileService
    {
        private readonly IFileProvider _fileProvider;

        public FileService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public Stream OpenRead(IFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            return _fileProvider.OpenRead(file);
        }
    }
}
