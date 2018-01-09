using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation.Entities;
using System.Linq;
using CustomEntityFoundation.Utilities;
using DotNetToolkit;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Text")]
    public class TextField : FieldRepository
    {
        [DataType(DataType.Text)]
        [MaxLength(512)]
        public String Text { get; set; }

        public override FieldRepository ConvertToField(JToken jToken, Type joType)
        {
            return new TextField
            {
                Text = jToken?.ToString()
            };
        }

        public override object GetFieldData(Object data)
        {
            return data == null ? String.Empty : data.ToObject<TextField>()?.Text;
        }
    }
}
