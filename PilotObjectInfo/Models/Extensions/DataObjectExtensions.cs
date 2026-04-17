using System;
using System.Collections.Generic;
using System.Linq;
using Ascon.Pilot.SDK;
using PilotObjectInfo.Models.Mapping;

namespace PilotObjectInfo.Models.Extensions
{
    /// <summary>
    /// Extension methods for convenient conversion from SDK interfaces to POCO models
    /// </summary>
    public static class DataObjectExtensions
    {
        /// <summary>
        /// Converts IDataObject to DataObject model
        /// </summary>
        public static Core.DataObject ToModel(this IDataObject source)
        {
            return DataObjectMapper.ToDataObject(source);
        }

        /// <summary>
        /// Converts collection of IDataObject to collection of DataObject models
        /// </summary>
        public static List<Core.DataObject> ToModels(this IEnumerable<IDataObject> source)
        {
            if (source == null)
                return new List<Core.DataObject>();

            return source.Select(obj => DataObjectMapper.ToDataObject(obj)).Where(obj => obj != null).ToList();
        }

        /// <summary>
        /// Converts IType to TypeInfo model
        /// </summary>
        public static Core.TypeInfo ToModel(this IType source)
        {
            return DataObjectMapper.ToTypeInfo(source);
        }

        /// <summary>
        /// Converts collection of IType to collection of TypeInfo models
        /// </summary>
        public static List<Core.TypeInfo> ToModels(this IEnumerable<IType> source)
        {
            if (source == null)
                return new List<Core.TypeInfo>();

            return source.Select(t => DataObjectMapper.ToTypeInfo(t)).Where(t => t != null).ToList();
        }

        /// <summary>
        /// Converts IPerson to Person model
        /// </summary>
        public static Core.Person ToModel(this IPerson source)
        {
            return DataObjectMapper.ToPerson(source);
        }

        /// <summary>
        /// Converts collection of IPerson to collection of Person models
        /// </summary>
        public static List<Core.Person> ToModels(this IEnumerable<IPerson> source)
        {
            if (source == null)
                return new List<Core.Person>();

            return source.Select(p => DataObjectMapper.ToPerson(p)).Where(p => p != null).ToList();
        }

        /// <summary>
        /// Converts IStateInfo to StateInfo model
        /// </summary>
        public static Core.StateInfo ToModel(this IStateInfo source)
        {
            return DataObjectMapper.ToStateInfo(source);
        }

        /// <summary>
        /// Converts IFile to PilotFile model
        /// </summary>
        public static Core.PilotFile ToPilotFile(this IFile source)
        {
            return DataObjectMapper.ToPilotFile(source);
        }

        /// <summary>
        /// Converts collection of IFile to collection of PilotFile models
        /// </summary>
        public static List<Core.PilotFile> ToPilotFiles(this IEnumerable<IFile> source)
        {
            if (source == null)
                return new List<Core.PilotFile>();

            return source.Select(f => DataObjectMapper.ToPilotFile(f)).Where(f => f != null).ToList();
        }

        /// <summary>
        /// Converts IFilesSnapshot to FilesSnapshot model
        /// </summary>
        public static Core.FilesSnapshot ToModel(this IFilesSnapshot source)
        {
            return DataObjectMapper.ToFilesSnapshot(source);
        }

        /// <summary>
        /// Converts collection of IFilesSnapshot to collection of FilesSnapshot models
        /// </summary>
        public static List<Core.FilesSnapshot> ToModels(this IEnumerable<IFilesSnapshot> source)
        {
            if (source == null)
                return new List<Core.FilesSnapshot>();

            return source.Select(s => DataObjectMapper.ToFilesSnapshot(s)).Where(s => s != null).ToList();
        }

        /// <summary>
        /// Converts IRelation to RelationInfo model
        /// </summary>
        public static Core.RelationInfo ToModel(this IRelation source)
        {
            return DataObjectMapper.ToRelationInfo(source);
        }

        /// <summary>
        /// Converts collection of IRelation to collection of RelationInfo models
        /// </summary>
        public static List<Core.RelationInfo> ToModels(this IEnumerable<IRelation> source)
        {
            if (source == null)
                return new List<Core.RelationInfo>();

            return source.Select(r => DataObjectMapper.ToRelationInfo(r)).Where(r => r != null).ToList();
        }

        /// <summary>
        /// Converts IAccessRecord to AccessRecord model
        /// </summary>
        public static Core.AccessRecord ToModel(this IAccessRecord source)
        {
            return DataObjectMapper.ToAccessRecord(source);
        }

        /// <summary>
        /// Converts collection of IAccessRecord to collection of AccessRecord models
        /// </summary>
        public static List<Core.AccessRecord> ToModels(this IEnumerable<IAccessRecord> source)
        {
            if (source == null)
                return new List<Core.AccessRecord>();

            return source.Select(a => DataObjectMapper.ToAccessRecord(a)).Where(a => a != null).ToList();
        }

        /// <summary>
        /// Converts IOrganisationUnit to OrganisationUnit model
        /// </summary>
        public static Support.OrganisationUnit ToModel(this IOrganisationUnit source)
        {
            return DataObjectMapper.ToOrganisationUnit(source);
        }

        /// <summary>
        /// Converts collection of IOrganisationUnit to collection of OrganisationUnit models
        /// </summary>
        public static List<Support.OrganisationUnit> ToModels(this IEnumerable<IOrganisationUnit> source)
        {
            if (source == null)
                return new List<Support.OrganisationUnit>();

            return source.Select(ou => DataObjectMapper.ToOrganisationUnit(ou)).Where(ou => ou != null).ToList();
        }

        /// <summary>
        /// Converts IUserState to UserStateInfo model
        /// </summary>
        public static Core.UserStateInfo ToModel(this IUserState source)
        {
            return DataObjectMapper.ToUserStateInfo(source);
        }

        /// <summary>
        /// Converts collection of IUserState to collection of UserStateInfo models
        /// </summary>
        public static List<Core.UserStateInfo> ToModels(this IEnumerable<IUserState> source)
        {
            if (source == null)
                return new List<Core.UserStateInfo>();

            return source.Select(us => DataObjectMapper.ToUserStateInfo(us)).Where(us => us != null).ToList();
        }

        /// <summary>
        /// Converts ISignatureRequest to SignatureRequestInfo model
        /// </summary>
        public static Core.SignatureRequestInfo ToModel(this ISignatureRequest source)
        {
            return DataObjectMapper.ToSignatureRequestInfo(source);
        }

        /// <summary>
        /// Converts collection of ISignatureRequest to collection of SignatureRequestInfo models
        /// </summary>
        public static List<Core.SignatureRequestInfo> ToModels(this IEnumerable<ISignatureRequest> source)
        {
            if (source == null)
                return new List<Core.SignatureRequestInfo>();

            return source.Select(sr => DataObjectMapper.ToSignatureRequestInfo(sr)).Where(sr => sr != null).ToList();
        }
    }
}

