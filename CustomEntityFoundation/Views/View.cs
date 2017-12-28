using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Models;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Views
{
    public class View : Entity, IDbRecord
    {
        public String Name { get; set; }

        [StringLength(36)]
        public string BundleId { get; set; }

        [ForeignKey("BundleId")]
        public Bundle Bundle { get; set; }

        [NotMapped]
        public PageResult<Object> Result { get; set; }

        public List<ViewColum> Columns { get; set; }

        public List<ViewFilter> Filters { get; set; }

        public override bool IsExist<T>(Database dc)
        {
            return dc.Table<View>().Any(x => x.Name == Name);
        }
    }
}
