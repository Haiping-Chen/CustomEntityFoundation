using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Utilities;
using DotNetToolkit;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomEntityFoundation.Fields
{
    public class FieldRepository : Entity, IFieldRepository
    {
        [Required]
        [StringLength(36)]
        public string BundleFieldId { get; set; }

        [Required]
        [StringLength(36)]
        public string EntityId { get; set; }

        /// <summary>
        /// Get base field types
        /// </summary>
        /// <returns>list of base field type, like TextField</returns>
        public static List<Type> FieldTypes()
        {
            var fieldRepositories = TypeHelper.GetClassesWithInterface<IFieldRepository>(Database.Assemblies).ToList();
            return fieldRepositories.Select(x => x.BaseType).Distinct().ToList();
        }

        /// <summary>
        /// Get base field types in string
        /// </summary>
        /// <returns>list of name, like Text</returns>
        public static List<String> FieldTypeNames()
        {
            var types = FieldTypes();

            return types.Select(x => (x.GetCustomAttributes(typeof(FieldTypeAttribute), false).FirstOrDefault()))
                .Where(x => x != null).Select(x => (x as FieldTypeAttribute).GetFieldTypeName())
                .ToList();
        }

        public List<Object> Load(Database dc, String entityName, String fieldTypeName)
        {
            var fields = dc.Table($"{entityName}{fieldTypeName}Field").Where(x => (x as FieldRepository).EntityId == EntityId && (x as FieldRepository).BundleFieldId == BundleFieldId).ToList();
            return fields.Select(x => (Object)x).ToList();
        }

        public List<Object> Extract(String entityId, FieldInBundle field, JToken jo, Type joType)
        {
            var objects = new List<Object>();

            if (jo == null) return objects;

            // while field.IsMultiple == true
            if (jo.Type == JTokenType.Array)
            {
                jo.ToList().ForEach(j =>
                {
                    var obj = ConvertToField(j, joType);

                    obj.BundleFieldId = field.Id;
                    obj.Id = Guid.NewGuid().ToString();
                    obj.EntityId = entityId;

                    objects.Add(JObject.FromObject(obj));
                });
            }
            else
            {
                var obj = ConvertToField(jo, joType);

                obj.BundleFieldId = field.Id;
                obj.Id = Guid.NewGuid().ToString();
                obj.EntityId = entityId;

                objects.Add(JObject.FromObject(obj));
            }

            return objects;
        }

        public virtual FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return jToken.ToObject(joType) as FieldRepository;
        }

        public Object ToBusinessObject(FieldInBundle field)
        {
            List<Object> jo = new List<Object>();

            field.Records.ForEach(data =>
            {
                var record = GetFieldData(data);
                jo.Add(record);
            });

            return field.IsMultiple ? jo : jo.FirstOrDefault();
        }

        public virtual Object GetFieldData(Object data)
        {
            var d = JObject.FromObject(data);

            d.Remove("BundleFieldId");
            d.Remove("EntityId");
            d.Remove("UpdatedTime");
            d.Remove("Status");
            d.Remove("Id");

            return d;
        }
    }
}
