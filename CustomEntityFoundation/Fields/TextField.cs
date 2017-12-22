using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation.Entities;
using System.Linq;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Text")]
    public class TextField : FieldRepository
    {
        [DataType(DataType.Text)]
        [MaxLength(512)]
        public String Text { get; set; }

        protected override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new TextField
            {
                Text = jToken?.ToString()
            };
        }

        protected override object GetFieldData(JObject data)
        {
            return data.ToObject<TextField>()?.Text;
        }
    }
}
