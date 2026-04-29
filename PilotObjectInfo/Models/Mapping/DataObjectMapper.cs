using System;
using System.Collections.Generic;
using System.Linq;
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Data;

namespace PilotObjectInfo.Models.Mapping
{
    /// <summary>
    /// Static mapper for converting Ascon.Pilot.SDK interfaces to POCO model classes
    /// </summary>
    public static class DataObjectMapper
    {
        /// <summary>
        /// Converts IDataObject to DataObject
        /// </summary>
        public static Core.DataObject ToDataObject(this IDataObject source)
        {
            if (source == null)
                return null;

            var result = new Core.DataObject
            {
                Id = source.Id,
                ParentId = source.ParentId,
                Created = source.Created,
                DisplayName = source.DisplayName,
                Type = source.Type?.ToTypeInfo(),
                Creator = source.Creator?.ToPerson(),
                ModifiedBy = source.ModifiedBy?.ToPerson(),
                ModifiedDate = source.ModifiedDate,
                State = source.State,
                ObjectStateInfo = source.ObjectStateInfo?.ToStateInfo(),
                SynchronizationState = source.SynchronizationState,
                IsSecret = source.IsSecret,
                IsDeleted = source.IsDeleted,
                IsInRecycleBin = source.IsInRecycleBin,
                ActualFileSnapshot = source.ActualFileSnapshot?.ToFilesSnapshot(),
                LockInfo = source.LockInfo, // Keep as object,
                HistoryItems = source.HistoryItems()
            };

            // Map collections
            if (source.Attributes != null)
            {
                result.Attributes = new Dictionary<string, object>(source.Attributes);
            }

            if (source.Children != null)
            {
                result.Children = source.Children.ToList();
            }

            if (source.Relations != null)
            {
                result.Relations = source.Relations.Select(r => r?.ToRelationInfo()).Where(r => r != null).ToList();
            }

            if (source.RelatedSourceFiles != null)
            {
                result.RelatedSourceFiles = source.RelatedSourceFiles.ToList();
            }

            if (source.RelatedTaskInitiatorAttachments != null)
            {
                result.RelatedTaskInitiatorAttachments = source.RelatedTaskInitiatorAttachments.ToList();
            }

            if (source.RelatedTaskExecutorAttachments != null)
            {
                result.RelatedTaskExecutorAttachments = source.RelatedTaskExecutorAttachments.ToList();
            }

            if (source.RelatedTaskMessageAttachments != null)
            {
                result.RelatedTaskMessageAttachments = source.RelatedTaskMessageAttachments.ToList();
            }

            if (source.TypesByChildren != null)
            {
                result.TypesByChildren = new Dictionary<Guid, int>(source.TypesByChildren);
            }

            if (source.Files != null)
            {
                result.Files = source.Files.Select(f => f?.ToPilotFile()).Where(f => f != null).ToList();
            }

            if (source.Access != null)
            {
                result.Access = source.Access.ToDictionary(kv => kv.Key, kv => (object)kv.Value);
            }

            if (source.Access2 != null)
            {
                result.Access2 = source.Access2.Select(a => a?.ToAccessRecord()).Where(a => a != null).ToList();
            }

            if (source.PreviousFileSnapshots != null)
            {
                result.PreviousFileSnapshots = source.PreviousFileSnapshots.Select(s => s?.ToFilesSnapshot()).Where(s => s != null).ToList();
            }

            if (source.Subscribers != null)
            {
                result.Subscribers = source.Subscribers.ToList();
            }

            return result;
        }

        /// <summary>
        /// Converts IType to TypeInfo
        /// </summary>
        public static Core.TypeInfo ToTypeInfo(this IType source)
        {
            if (source == null)
                return null;

            var result = new Core.TypeInfo
            {
                Id = source.Id,
                Name = source.Name,
                Title = source.Title,
                Kind = source.Kind,
                IsMountable = source.IsMountable,
                IsProject = source.IsProject,
                IsService = source.IsService
            };

            if (source.Attributes != null)
            {
                result.Attributes = source.Attributes
                    .Select(a => a?.ToAttributeTypeInfo())
                    .Where(a => a != null)
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Converts IPerson to Person
        /// </summary>
        public static Core.Person ToPerson(this IPerson source)
        {
            if (source == null)
                return null;

            return new Core.Person
            {
                Id = source.Id,
                ActualName = source.ActualName,
                DisplayName = source.DisplayName,
                IsAdmin = source.IsAdmin,
                Login = source.Login,
                Sid = source.Sid,
                MainPosition = source.MainPosition?.ToPositionInfo()
            };
        }

        /// <summary>
        /// Converts IStateInfo to StateInfo
        /// </summary>
        public static Core.StateInfo ToStateInfo(this IStateInfo source)
        {
            if (source == null)
                return null;

            return new Core.StateInfo
            {
                State = source.State,
                Date = source.Date,
                PersonId = source.PersonId,
                PositionId = source.PositionId
            };
        }

        /// <summary>
        /// Converts IFile to PilotFile
        /// </summary>
        public static Core.PilotFile ToPilotFile(this IFile source)
        {
            if (source == null)
                return null;

            var result = new Core.PilotFile
            {
                Id = source.Id,
                Name = source.Name,
                Size = source.Size,
                Created = source.Created,
                OriginalFile = source // Store original reference for file operations
            };

            if (source.SignatureRequests != null)
            {
                // Simple conversion - just capture ID for now
                result.SignatureRequests = source.SignatureRequests
                    .Select((sr, index) => new Core.SignatureRequestInfo { Id = index })
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Converts IFilesSnapshot to FilesSnapshot
        /// </summary>
        public static Core.FilesSnapshot ToFilesSnapshot(this IFilesSnapshot source)
        {
            if (source == null)
                return null;

            var result = new Core.FilesSnapshot
            {
                Created = source.Created,
                CreatorId = source.CreatorId,
                Reason = source.Reason
            };

            if (source.Files != null)
            {
                result.Files = source.Files.Select(f => f?.ToPilotFile()).Where(f => f != null).ToList();
            }

            return result;
        }

        /// <summary>
        /// Converts IRelation to RelationInfo
        /// </summary>
        public static Core.RelationInfo ToRelationInfo(this IRelation source)
        {
            if (source == null)
                return null;

            return new Core.RelationInfo
            {
                TargetId = source.TargetId
            };
        }

        /// <summary>
        /// Converts IAccessRecord to AccessRecord
        /// </summary>
        public static Core.AccessRecord ToAccessRecord(this IAccessRecord source)
        {
            if (source == null)
                return null;

            return new Core.AccessRecord
            {
                // Properties will be added when interface structure is clarified
            };
        }

        /// <summary>
        /// Converts IAttribute to AttributeTypeInfo
        /// </summary>
        public static Support.AttributeTypeInfo ToAttributeTypeInfo(this IAttribute source)
        {
            if (source == null)
                return null;

            return new Support.AttributeTypeInfo
            {
                Name = source.Name,
                Type = source.Type
            };
        }

        /// <summary>
        /// Converts IPosition to PositionInfo
        /// </summary>
        public static Support.PositionInfo ToPositionInfo(this IPosition source)
        {
            if (source == null)
                return null;

            return new Support.PositionInfo
            {
                PositionId = source.Position
            };
        }

        /// <summary>
        /// Converts IOrganisationUnit to OrganisationUnit
        /// </summary>
        public static Support.OrganisationUnit ToOrganisationUnit(this IOrganisationUnit source)
        {
            if (source == null)
                return null;

            var result = new Support.OrganisationUnit
            {
                Id = source.Id,
                Title = source.Title,
                IsPosition = source.IsPosition,
                IsChief = source.IsChief,
                IsDeleted = source.IsDeleted
            };

            if (source.Children != null)
            {
                result.Children = source.Children.ToList();
            }

            return result;
        }

        /// <summary>
        /// Converts IUserState to UserStateInfo
        /// </summary>
        public static Core.UserStateInfo ToUserStateInfo(this IUserState source)
        {
            if (source == null)
                return null;

            return new Core.UserStateInfo
            {
                Id = source.Id,
                Title = source.Title,
                Name = source.Name,
                IsDeleted = source.IsDeleted
            };
        }

        /// <summary>
        /// Converts ISignatureRequest to SignatureRequestInfo
        /// </summary>
        public static Core.SignatureRequestInfo ToSignatureRequestInfo(this ISignatureRequest source)
        {
            if (source == null)
                return null;

            // Simple mapping - ISignatureRequest structure may vary
            return new Core.SignatureRequestInfo
            {
                Id = source.GetHashCode() // Use hashcode as simple ID
            };
        }

        /// <summary>
        /// Converts IHistoryItem to HistoryItem
        /// </summary>
        public static Core.HistoryItem ToHistoryItem(this IHistoryItem source)
        {
            if (source == null)
                return null;

            return new Core.HistoryItem
            {
                Id = source.Id,
                Created = source.Created,
                Reason = source.Reason,
                ObjectId = source.Object?.Id ?? Guid.Empty,
                DisplayName = source.Object?.DisplayName ?? string.Empty,
                Object = source.Object?.ToDataObject()
            };
        }
    }
}

