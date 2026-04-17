using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK;
using PilotObjectInfo.Models.Extensions;
using PilotObjectInfo.Models.Core;

namespace PilotObjectInfo.Models.Examples
{
    /// <summary>
    /// Usage examples for DataObject models and mapping
    /// </summary>
    public class DataObjectMappingExamples
    {
        /// <summarysingle>
        /// Example 1: Convert single IDataObject to DataObject model
        /// </summary>
        public static DataObject ConvertSingleObject(IDataObject source)
        {
            if (source == null)
                return null;

            // Using extension method - Recommended way
            var model = source.ToModel();

            // Now you can work with the detached model object
            Console.WriteLine($"ID: {model.Id}");
            Console.WriteLine($"Display Name: {model.DisplayName}");
            Console.WriteLine($"Type: {model.Type?.Name}");
            Console.WriteLine($"Creator: {model.Creator?.DisplayName}");
            Console.WriteLine($"Created: {model.Created}");
            Console.WriteLine($"Is Secret: {model.IsSecret}");

            return model;
        }

        /// <summary>
        /// Example 2: Convert collection of IDataObject to list of DataObject models
        /// </summary>
        public static List<DataObject> ConvertObjects(IEnumerable<IDataObject> source)
        {
            // Using extension method for collections
            var models = source.ToModels();

            // Now you have a list of detached model objects
            foreach (var model in models)
            {
                ProcessModel(model);
            }

            return models;
        }

        /// <summary>
        /// Example 3: Convert IType to TypeInfo model
        /// </summary>
        public static TypeInfo ConvertType(IType source)
        {
            var typeModel = source.ToModel();

            Console.WriteLine($"Type ID: {typeModel.Id}");
            Console.WriteLine($"Type Name: {typeModel.Name}");
            Console.WriteLine($"Type Title: {typeModel.Title}");
            Console.WriteLine($"Is Project: {typeModel.IsProject}");
            Console.WriteLine($"Is Service: {typeModel.IsService}");
            Console.WriteLine($"Attributes count: {typeModel.Attributes.Count}");

            return typeModel;
        }

        /// <summary>
        /// Example 4: Convert IPerson to Person model
        /// </summary>
        public static Person ConvertPerson(IPerson source)
        {
            var personModel = source.ToModel();

            Console.WriteLine($"Person ID: {personModel.Id}");
            Console.WriteLine($"Display Name: {personModel.DisplayName}");
            Console.WriteLine($"Login: {personModel.Login}");
            Console.WriteLine($"Is Admin: {personModel.IsAdmin}");
            Console.WriteLine($"Main Position: {personModel.MainPosition?.PositionId}");

            return personModel;
        }

        /// <summary>
        /// Example 5: Work with files (note: use ToPilotFile to avoid System.IO.File conflict)
        /// </summary>
        public static List<PilotFile> ConvertFiles(IEnumerable<IFile> source)
        {
            // Special extension method for IFile to avoid conflict with System.IO.File
            var fileModels = source.ToPilotFiles();

            foreach (var fileModel in fileModels)
            {
                Console.WriteLine($"File: {fileModel.Name}");
                Console.WriteLine($"Size: {fileModel.Size} bytes");
                Console.WriteLine($"Created: {fileModel.Created}");
            }

            return fileModels;
        }

        /// <summary>
        /// Example 6: Convert IFilesSnapshot to FilesSnapshot model
        /// </summary>
        public static FilesSnapshot ConvertSnapshot(IFilesSnapshot source)
        {
            var snapshotModel = source.ToModel();

            Console.WriteLine($"Snapshot Created: {snapshotModel.Created}");
            Console.WriteLine($"Creator ID: {snapshotModel.CreatorId}");
            Console.WriteLine($"Reason: {snapshotModel.Reason}");
            Console.WriteLine($"Files count: {snapshotModel.Files.Count}");

            return snapshotModel;
        }

        /// <summary>
        /// Example 7: Convert IRelation to Relation model
        /// </summary>
        public static List<RelationInfo> ConvertRelations(IEnumerable<IRelation> source)
        {
            var relationModels = source.ToModels();

            foreach (var relation in relationModels)
            {
                Console.WriteLine($"Target ID: {relation.TargetId}");
            }

            return relationModels;
        }

        /// <summary>
        /// Example 8: Recursive mapping - all nested objects are automatically converted
        /// </summary>
        public static void DemonstrateRecursiveMapping(IDataObject source)
        {
            var model = source.ToModel();

            // All nested objects are automatically converted recursively
            if (model.Type != null)
            {
                Console.WriteLine($"Type (converted from IType): {model.Type.Name}");
            }

            if (model.Creator != null)
            {
                Console.WriteLine($"Creator (converted from IPerson): {model.Creator.DisplayName}");
                
                if (model.Creator.MainPosition != null)
                {
                    Console.WriteLine($"Creator's position (converted from IPosition): {model.Creator.MainPosition.PositionId}");
                }
            }

            if (model.ObjectStateInfo != null)
            {
                Console.WriteLine($"Object State (converted from IStateInfo): {model.ObjectStateInfo.State}");
            }

            // Collections are also recursively converted
            foreach (var relation in model.Relations)
            {
                Console.WriteLine($"Relation (converted from IRelation): TargetId={relation.TargetId}");
            }

            foreach (var file in model.Files)
            {
                Console.WriteLine($"File (converted from IFile): {file.Name}");
            }

            foreach (var accessRecord in model.Access2)
            {
                Console.WriteLine($"Access Record (converted from IAccessRecord)");
            }
        }

        /// <summary>
        /// Example 9: Working with detached model - serialize to JSON
        /// </summary>
        public static string SerializeModelToJson(IDataObject source)
        {
            var model = source.ToModel();

            // Model is now detached from the interface and can be serialized
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        /// <summary>
        /// Example 10: Processing model in separate layer/service
        /// </summary>
        private static void ProcessModel(DataObject model)
        {
            // Model can be processed independently of SDK interfaces
            // This allows for loose coupling and better testability
            
            if (model.IsSecret)
            {
                Console.WriteLine("This is a secret object");
            }

            if (model.Type != null && model.Type.IsProject)
            {
                Console.WriteLine("This is a project object");
            }

            // You can create data transfer objects (DTOs) from models
            var dto = new MyDataDto
            {
                Id = model.Id,
                Name = model.DisplayName,
                TypeName = model.Type?.Name,
                CreatorName = model.Creator?.DisplayName,
                ChildrenCount = model.Children.Count,
                FilesCount = model.Files.Count
            };

            Console.WriteLine($"DTO: {dto}");
        }
    }

    /// <summary>
    /// Example DTO for demonstration
    /// </summary>
    public class MyDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string CreatorName { get; set; }
        public int ChildrenCount { get; set; }
        public int FilesCount { get; set; }

        public override string ToString()
        {
            return $"ID={Id}, Name={Name}, Type={TypeName}, Creator={CreatorName}, Children={ChildrenCount}, Files={FilesCount}";
        }
    }
}

