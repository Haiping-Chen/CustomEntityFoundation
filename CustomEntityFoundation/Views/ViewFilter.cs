using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomEntityFoundation.Views
{
    public class ViewFilter : Entity, IDbRecord
    {
        [JsonIgnore]
        [StringLength(36)]
        public String ViewId { get; set; }

        [StringLength(36)]
        public String FieldName { get; set; }

        [StringLength(64)]
        public String Value { get; set; }

        public Boolean ExposedToUser { get; set; }

        public FieldInBundle Field { get; set; }

        [NotMapped]
        public String BundleFieldId { get; set; }

        [NotMapped]
        public String FieldType { get; set; }

        [NotMapped]
        public IEnumerable<JObject> Data { get; set; }
    }
}
