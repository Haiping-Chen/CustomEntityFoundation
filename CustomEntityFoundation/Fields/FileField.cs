using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CustomEntityFoundation.Fields
{
    [FieldType("File")]
    public class FileField : FieldRepository
    {
        [MaxLength(64)]
        public string Title { get; set; }

        public int Size { get; set; }

        [MaxLength(256)]
        public string Path { get; set; }

        public override object GetFieldData(object data)
        {
            return data == null ? new FileField() : base.GetFieldData(data);
        }
    }
}
