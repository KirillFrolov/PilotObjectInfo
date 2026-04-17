using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IFile
    /// </summary>
    public class PilotFile
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public long Size { get; set; }
        
        public DateTime Created { get; set; }
        
        public List<SignatureRequestInfo> SignatureRequests { get; set; } = new List<SignatureRequestInfo>();
        
        /// <summary>
        /// Original SDK IFile reference for operations that require it (e.g., file streaming)
        /// </summary>
        public IFile OriginalFile { get; set; }
    }
}
