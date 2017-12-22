using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CustomEntityFoundation.Entities;
using Newtonsoft.Json.Linq;

namespace CustomEntityFoundation.Fields
{
    [FieldType("DateTime")]
    public class DateTimeField : FieldRepository
    {
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        protected override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new DateTimeField
            {
                DateTime = jToken.ToObject<DateTime>()
            };
        }

        protected override object GetFieldData(JObject data)
        {
            return data.ToObject<DateTimeField>()?.DateTime;
        }
    }
}
