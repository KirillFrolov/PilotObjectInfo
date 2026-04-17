using System;
using System.Collections.Generic;

namespace PilotObjectInfo.Models.Support
{
    /// <summary>
    /// Wrapper class for IOrganisationUnit
    /// </summary>
    public class OrganisationUnit
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public bool IsPosition { get; set; }
        
        public bool IsChief { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public List<int> Children { get; set; } = new List<int>();
    }
}

