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

        protected override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new BooleanField
            {
                Value = jToken.ToObject<Boolean>()
            };
        }

        protected override object GetFieldData(JObject data)
        {
            return data.ToObject<BooleanField>()?.Value;
        }
    }
}
