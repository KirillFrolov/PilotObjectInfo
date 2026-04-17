using PilotObjectInfo.Models.Support;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IPerson
    /// </summary>
    public class Person
    {
        public int? Id { get; set; }
        
        public string ActualName { get; set; }
        
        public string DisplayName { get; set; }
        
        public bool? IsAdmin { get; set; }
        
        public string Login { get; set; }
        
        public string Sid { get; set; }
        
        public PositionInfo MainPosition { get; set; }
    }
}

