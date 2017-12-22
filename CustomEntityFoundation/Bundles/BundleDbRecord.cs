using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using CustomEntityFoundation.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    public abstract class BundleDbRecord : Entity, IBundlableEntity
    {
        [Required]
        [StringLength(36)]
        public string BundleId { get; set; }

        [Required]
        [EntityPropertyAsField("Name", "Text")]
        [MaxLength(64, ErrorMessage = "Entity Name cannot be longer than 64 characters.")]
        public String Name { get; set; }

        [NotMapped]
        public List<FieldInBundle> Fields { get; set; }

        public virtual void LoadEntity(EntityDbContext dc, String entityName)
        {
            LoadFieldsDefinition(dc);

            var types = TypeHelper.GetClassesWithInterface<IFieldRepository>(EntityDbContext.Assembles)
                .Where(x => x.Name.StartsWith(entityName) && x.Name.EndsWith("Field")).ToList();

            types.ForEach(fieldType => LoadFieldRecords(dc, entityName, fieldType));
        }

        private String GetFieldTypeName<TFieldRepository>()
        {
            FieldTypeAttribute fieldType = typeof(TFieldRepository).BaseType.GetCustomAttributes(typeof(FieldTypeAttribute), false).First() as FieldTypeAttribute;
            return fieldType.GetFieldTypeName();
        }

        private void LoadFieldRecords(EntityDbContext dc, string entityName, Type fieldType)
        {
            FieldTypeAttribute fieldTypeAttribute = fieldType.BaseType.GetCustomAttributes(typeof(FieldTypeAttribute), false).First() as FieldTypeAttribute;
            String fieldTypeName = fieldTypeAttribute.GetFieldTypeName();

            Fields.Where(x => x.FieldTypeName == fieldTypeName)
                .ToList()
                .ForEach(field =>
                {
                    var repository = new FieldRepository { BundleFieldId = field.Id, EntityId = Id };
                    field.FieldRecords = repository.Load(dc, entityName, field.FieldTypeName);
                });
        }

        private void AddFieldRecords(EntityDbContext dc, Type fieldType)
        {
            FieldTypeAttribute fieldTypeAttribute = fieldType.BaseType.GetCustomAttributes(typeof(FieldTypeAttribute), false).First() as FieldTypeAttribute;
            String fieldTypeName = fieldTypeAttribute.GetFieldTypeName();

            Fields.Where(x => x.FieldTypeName == fieldTypeName && x.FieldRecords != null)
                .ToList()
                .ForEach(field =>
                {
                    field.FieldRecords.ForEach(data =>
                    {
                        var repository = data.ToObject(fieldType);
                        dc.Add(repository);
                    });
                });
        }

        public virtual bool InsertEntity(EntityDbContext dc, String entityName)
        {
            if (dc.Table(entityName).Any(x => (x as Entity).Id == Id)) return false;

            dc.Add(this);

            LoadFieldsDefinition(dc);

            var types = TypeHelper.GetClassesWithInterface<IFieldRepository>(EntityDbContext.Assembles)
                .Where(x => x.Name.StartsWith(entityName) && x.Name.EndsWith("Field")).ToList();

            types.ForEach(fieldType => {
                AddFieldRecords(dc, fieldType);
            });

            return true;
        }

        public virtual JObject ToBusinessObject(EntityDbContext dc, String entityName)
        {
            JObject jo = JObject.FromObject(this);

            var types = TypeHelper.GetClassesWithInterface<IFieldRepository>(EntityDbContext.Assembles)
                .Where(x => x.Name.StartsWith(entityName) && x.Name.EndsWith("Field")).ToList();

            types.ForEach(fieldType => {
                ToBusinessObject(dc, fieldType, jo);
            });

            jo.Remove("Fields");
            jo.Remove("BundleId");

            return jo;
        }

        private void ToBusinessObject(EntityDbContext dc, Type fieldType, JObject businessObject)
        {
            LoadFieldsDefinition(dc);

            FieldTypeAttribute fieldTypeAttribute = fieldType.BaseType.GetCustomAttributes(typeof(FieldTypeAttribute), false).First() as FieldTypeAttribute;
            String fieldTypeName = fieldTypeAttribute.GetFieldTypeName();

            Fields.Where(x => x.FieldTypeName == fieldTypeName && x.FieldRecords != null)
                .ToList()
                .ForEach(field =>
                {
                    var fieldInstance = Activator.CreateInstance(fieldType) as FieldRepository;
                    var fieldValue = fieldInstance.ToBusinessObject(field);
                    businessObject.Add(field.Name, fieldValue == null ? null : JToken.FromObject(fieldValue));
                });
        }

        protected void ToBusinessObject<TFieldRepository>(EntityDbContext dc, JObject businessObject) where TFieldRepository : IFieldRepository
        {
            LoadFieldsDefinition(dc);

            Fields.Where(x => x.FieldTypeName == GetFieldTypeName<TFieldRepository>() && x.FieldRecords != null)
                .ToList()
                .ForEach(field =>
                {
                    var fieldInstance = Activator.CreateInstance<TFieldRepository>();
                    var fieldValue = fieldInstance.ToBusinessObject(field);
                    businessObject.Add(field.Name, fieldValue == null ? null :JToken.FromObject(fieldValue));
                });
        }

        public void LoadFieldsDefinition(EntityDbContext dc)
        {
            if (Fields == null)
            {
                Fields = dc.FieldInBundle.Where(x => x.BundleId == BundleId).ToList();
            }
        }
    }
}
