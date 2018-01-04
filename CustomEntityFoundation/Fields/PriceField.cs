using CustomEntityFoundation.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomEntityFoundation.Core.Fields
{
    [FieldType("Price")]
    public class PriceField : FieldRepository
    {
        public Decimal Price { get; set; }

        [MaxLength(8)]
        public string Currency { get; set; }

        public override object GetFieldData(object data)
        {
            return data == null ? new PriceField() : base.GetFieldData(data);
        }
    }
}
