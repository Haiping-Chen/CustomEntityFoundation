using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;
using CustomEntityFoundation.Nodes;
using CustomEntityFoundation.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation
{
    public partial class EntityDbContext
    {
        public DbSet<Bundle> Bundle { get { return Table<Bundle>(); } }
        public DbSet<FieldInBundle> FieldInBundle { get { return Table<FieldInBundle>(); } }

        public DbSet<Node> Node { get { return Table<Node>(); } }
        public DbSet<View> View { get { return Table<View>(); } }
    }
}
