using System;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IHistoryItem
    /// </summary>
    public class HistoryItem
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Reason { get; set; }

        public Guid ObjectId { get; set; }

        public string DisplayName { get; set; }

        public DataObject Object { get; set; }
    }
}

