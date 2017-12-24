using CustomEntityFoundation.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Boolean")]
    public class BooleanField : FieldRepository
    {
        public Boolean Value { get; set; }

        public override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new BooleanField
            {
                Value = jToken.ToObject<Boolean>()
            };
        }

        public override object GetFieldData(Object data)
        {
            return data.ToObject<BooleanField>()?.Value;
        }
    }
}
