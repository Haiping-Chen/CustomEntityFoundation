using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Views
{
    public class ViewColum : Entity, IDbRecord
    {
        [StringLength(36)]
        public String ViewId { get; set; }

        [MaxLength(64)]
        public String DisplayName { get; set; }

        public Boolean HideName { get; set; }

        /// <summary>
        /// Convert as View Field Name
        /// </summary>
        [MaxLength(36)]
        public String TargetName { get; set; }

        /// <summary>
        /// Bundle Field Name
        /// </summary>
        [MaxLength(36)]
        public String FieldName { get; set; }

        [MaxLength(32)]
        public String FieldType { get; set; }

        public override bool IsExist<T>(Database dc)
        {
            return dc.Table<ViewColum>().Any(x => x.ViewId == ViewId && x.FieldName == FieldName);
        }
    }
}
