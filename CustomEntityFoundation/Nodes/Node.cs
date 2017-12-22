using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;
using System;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.BootKit;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomEntityFoundation.Nodes
{
    public class Node : BundleDbRecord, IBundlableEntity, IDbRecord
    {
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        [EntityPropertyAsField("Description", "Text")]
        public String Description { get; set; }
    }
}
