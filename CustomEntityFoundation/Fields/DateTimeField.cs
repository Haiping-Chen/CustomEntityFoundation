using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Utilities;
using Newtonsoft.Json.Linq;

namespace CustomEntityFoundation.Fields
{
    [FieldType("DateTime")]
    public class DateTimeField : FieldRepository
    {
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        public override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new DateTimeField
            {
                DateTime = jToken.ToObject<DateTime>()
            };
        }

        public override object GetFieldData(Object data)
        {
            return data.ToObject<DateTimeField>()?.DateTime;
        }
    }
}
