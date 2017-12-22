using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace CustomEntityFoundation.Fields
{
    [FieldType("EntityReference")]
    public class EntityReferenceField : FieldRepository
    {
        /// <summary>
        /// Record Id in datatable
        /// </summary>
        [StringLength(36)]
        [Required]
        public String RefEntityId { get; set; }

        protected override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new EntityReferenceField
            {
                RefEntityId = jToken?.ToString()
            };
        }

        protected override object GetFieldData(JObject data)
        {
            return data.ToObject<EntityReferenceField>()?.RefEntityId;
        }
    }
}
