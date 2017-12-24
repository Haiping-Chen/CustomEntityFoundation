using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    public class ActionInBundle : Entity, IDbRecord
    {
        [StringLength(36)]
        [Required]
        public string BundleId { get; set; }

        public String Name { get; set; }

        public String Url { get; set; }
    }
}
