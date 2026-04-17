using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK;
using PilotObjectInfo.Models.Support;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IType
    /// </summary>
    public class TypeInfo
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Title { get; set; }
        
        public TypeKind Kind { get; set; }
        
        public bool IsMountable { get; set; }
        
        public bool IsProject { get; set; }
        
        public bool IsService { get; set; }
        
        public List<AttributeTypeInfo> Attributes { get; set; } = new List<AttributeTypeInfo>();
    }
}

