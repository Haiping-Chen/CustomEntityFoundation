using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Integer")]
    public class IntegerField : FieldRepository
    {
        public Int32 Number { get; set; }

        protected override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new IntegerField
            {
                Number = jToken.ToObject<Int32>()
            };
        }

        protected override object GetFieldData(JObject data)
        {
            var fieldData = data.ToObject<IntegerField>();
            return fieldData.Number;
        }
    }
}
