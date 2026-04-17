using System;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IStateInfo
    /// </summary>
    public class StateInfo
    {
        public ObjectState State { get; set; }
        
        public DateTime? Date { get; set; }
        
        public int? PersonId { get; set; }
        
        public int? PositionId { get; set; }
    }
}

