using System;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IUserState
    /// </summary>
    public class UserStateInfo
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Name { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
