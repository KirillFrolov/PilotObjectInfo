using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Models.Core
{
    /// <summary>
    /// Wrapper class for IDataObject
    /// </summary>
    public class DataObject
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public DateTime Created { get; set; }

        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        public string DisplayName { get; set; }

        public TypeInfo Type { get; set; }

        public Person Creator { get; set; }

        public Person ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public List<Guid> Children { get; set; } = new List<Guid>();

        public List<RelationInfo> Relations { get; set; } = new List<RelationInfo>();

        public List<Guid> RelatedSourceFiles { get; set; } = new List<Guid>();

        [Obsolete]
        public List<Guid> RelatedTaskInitiatorAttachments { get; set; } = new List<Guid>();

        [Obsolete]
        public List<Guid> RelatedTaskExecutorAttachments { get; set; } = new List<Guid>();

        [Obsolete]
        public List<Guid> RelatedTaskMessageAttachments { get; set; } = new List<Guid>();

        public Dictionary<Guid, int> TypesByChildren { get; set; } = new Dictionary<Guid, int>();

        public DataState State { get; set; }

        public StateInfo ObjectStateInfo { get; set; }

        public SynchronizationState SynchronizationState { get; set; }

        public List<PilotFile> Files { get; set; } = new List<PilotFile>();

        [Obsolete]
        public Dictionary<int, object> Access { get; set; } = new Dictionary<int, object>();

        public List<AccessRecord> Access2 { get; set; } = new List<AccessRecord>();

        public bool IsSecret { get; set; }

        [Obsolete("Property is deprecated. Use ObjectStateInfo instead.")]
        public bool IsDeleted { get; set; }

        [Obsolete("Property is deprecated. Use ObjectStateInfo instead.")]
        public bool IsInRecycleBin { get; set; }

        public FilesSnapshot ActualFileSnapshot { get; set; }

        public List<FilesSnapshot> PreviousFileSnapshots { get; set; } = new List<FilesSnapshot>();

        public List<int> Subscribers { get; set; } = new List<int>();

        [Obsolete("Property is deprecated. Use ObjectStateInfo instead.")]
        public object LockInfo { get; set; }
    }
}

