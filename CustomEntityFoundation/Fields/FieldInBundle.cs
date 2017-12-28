using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CustomEntityFoundation.Fields
{
    public class FieldInBundle : Entity, IDbRecord
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public String Name { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Caption cannot be longer than 60 characters.")]
        public String Caption { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public String Description { get; set; }

        [StringLength(36)]
        [Required]
        public string BundleId { get; set; }

        [Required]
        public String FieldTypeName { get; set; }

        public Boolean Required { get; set; }

        public Boolean Hidden { get; set; }

        public Boolean IsMultiple { get; set; }

        [NotMapped]
        public Boolean IsBuiltIn { get; set; }

        /// <summary>
        /// Only for EntityReference field type
        /// </summary>
        [StringLength(36)]
        public string RefBundleId { get; set; }

        [NotMapped]
        public List<Object> Records { get; set; }

        public override bool IsExist<T>(Database dc)
        {
            return dc.Table<FieldInBundle>().Any(x => x.BundleId == BundleId && x.Name == Name);
        }
    }
}
