using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using CustomEntityFoundation.Utilities;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    /// <summary>
    /// Bundle definition
    /// Entity + Fields = Bundle
    /// </summary>
    public class Bundle : Entity, IDbRecord
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public String Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public String Description { get; set; }

        [Required]
        [MaxLength(64)]
        public string EntityName { get; set; }

        [ForeignKey("BundleId")]
        public List<FieldInBundle> Fields { get; set; }

        [ForeignKey("BundleId")]
        public List<ActionInBundle> Actions { get; set; }

        public override bool IsExist<T>(EntityDbContext dc)
        {
            return dc.Table<Bundle>().Any(x => x.EntityName == EntityName && x.Name == Name);
        }

        public BundleDbRecord GetEntityInstance(EntityDbContext dc)
        {
            var instance = Activator.CreateInstance(GetEntityType(dc)) as BundleDbRecord;
            instance.BundleId = Id;
            instance.Fields = Fields;
            return instance;
        }

        public Type GetEntityType(EntityDbContext dc)
        {
            var entityName = dc.Table<Bundle>().Find(Id).EntityName;
            var entityType = TypeHelper.GetClassesWithInterface<IBundlableEntity>(EntityDbContext.Assembles).FirstOrDefault(x => x.Name == entityName);
            return entityType;
        }

        /// <summary>
        /// Get customized fields and entity properties
        /// </summary>
        /// <returns></returns>
        public List<FieldInBundle> GetFields(EntityDbContext dc)
        {
            var entityType = GetEntityType(dc);

            Fields = dc.FieldInBundle.Where(x => x.BundleId == Id).ToList();
            
            entityType.GetProperties().ToList().ForEach(p =>
            {
                var property = p.GetCustomAttribute<EntityPropertyAsField>();
                if(property != null)
                {
                    var field = (property as EntityPropertyAsField).GetBundleField();
                    field.Caption = field.Name;
                    field.BundleId = Id;
                    field.IsBuiltIn = true;
                    field.Required = p.GetCustomAttribute<RequiredAttribute>() != null;
                    Fields.Insert(0, field);
                }
            });

            return Fields;
        }

        public IQueryable<BundleDbRecord> QueryRecords(EntityDbContext dc)
        {
            var type = TypeHelper.GetClassesWithInterface<IBundlableEntity>(EntityDbContext.Assembles).FirstOrDefault(x => x.Name == EntityName);

            if(type == null)
            {
                throw new Exception($"Can't find entity: {EntityName}");
            }

            return dc.Table(EntityName).Where(x => (x as BundleDbRecord).BundleId == Id).Select(x => x.ToObject(type) as BundleDbRecord);
        }

        public BundleDbRecord LoadRecord(EntityDbContext dc, String entityId)
        {
            var entityType = GetEntityType(dc);
            var record = dc.Find(entityType, entityId) as BundleDbRecord;
            record.LoadEntity(dc, EntityName);

            return record;
        }

        /// <summary>
        /// Add bundlable entity record
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="jEntity"></param>
        public BundleDbRecord AddRecord(EntityDbContext dc, JObject jEntity)
        {
            var type = TypeHelper.GetClassesWithInterface<IBundlableEntity>(EntityDbContext.Assembles).FirstOrDefault(x => x.Name == EntityName);

            // extract data to main object, fill field record later.
            var record = jEntity.ToObject(type) as BundleDbRecord;

            record.BundleId = Id;
            record.Id = record.Id ?? Guid.NewGuid().ToString();
            record.Fields = Fields;

            // fill custom fields
            record.Fields.ForEach(field =>
            {
                var fieldType = TypeHelper.GetClassesWithInterface<IFieldRepository>(EntityDbContext.Assembles).FirstOrDefault(x => x.Name == field.FieldTypeName + "Field");
                if (fieldType == null)
                {
                    Console.WriteLine($"{field.FieldTypeName} field not found. Ignored {field.Name} column. {field.Id}");
                }
                else
                {
                    var fieldInstance = (FieldRepository)Activator.CreateInstance(fieldType);
                    fieldInstance.BundleFieldId = field.Id;
                    field.Records = fieldInstance.Extract(record.Id, field, jEntity[field.Name], fieldType);
                }
            });

            record.InsertEntity(dc, record.GetEntityName());

            return record;
        }
    }
}
