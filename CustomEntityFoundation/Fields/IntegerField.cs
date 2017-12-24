using CustomEntityFoundation.Utilities;
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

        public override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new IntegerField
            {
                Number = jToken.ToObject<Int32>()
            };
        }

        public override object GetFieldData(Object data)
        {
            var fieldData = data.ToObject<IntegerField>();
            return fieldData.Number;
        }
    }
}
