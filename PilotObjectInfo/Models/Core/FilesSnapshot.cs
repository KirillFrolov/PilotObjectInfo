using System;
using System.Collections.Generic;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IFilesSnapshot
    /// </summary>
    public class FilesSnapshot
    {
        public DateTime Created { get; set; }
        
        public int CreatorId { get; set; }
        
        public string Reason { get; set; }
        
        public List<PilotFile> Files { get; set; } = new List<PilotFile>();
    }
}

