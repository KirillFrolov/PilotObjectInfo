using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IAccessRecord
    /// </summary>
    public class AccessRecord
    {
        public int OrgUnitId { get; set; }
        
        public AccessLevel AccessLevel { get; set; }
        
        public bool IsInheritable { get; set; }
    }
}

