using CustomEntityFoundation.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    [FieldType("Address")]
    public class AddressField : FieldRepository
    {
        [MaxLength(256)]
        public string AddressLine1 { get; set; }

        [MaxLength(256)]
        public string AddressLine2 { get; set; }

        [MaxLength(10)]
        public string Zipcode { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        [MaxLength(64)]
        public string County { get; set; }

        [MaxLength(64)]
        public string State { get; set; }

        [MaxLength(64)]
        public string Country { get; set; }

        [NotMapped]
        public string Address
        {
            get
            {
                return $"{AddressLine1} {AddressLine2}, {City}, {State} {Zipcode}, {Country}";
            }
        }

        public override object GetFieldData(object data)
        {
            return data == null ? new FileField() : base.GetFieldData(data);
        }
    }
}
