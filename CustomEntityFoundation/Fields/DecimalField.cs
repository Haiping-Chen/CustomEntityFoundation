using CustomEntityFoundation.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Decimal")]
    public class DecimalField : FieldRepository
    {
        public Decimal Number { get; set; }

        public override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new DecimalField
            {
                Number = jToken.ToObject<Decimal>()
            };
        }

        public override object GetFieldData(Object data)
        {
            var fieldData = data.ToObject<DecimalField>();
            return fieldData.Number;
        }
    }
}
