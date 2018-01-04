using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    public static class BundleExtension
    {
        public static FieldInBundle AddField(this Bundle bundle, Database dc, FieldInBundle field)
        {
            if (field.IsExist<FieldInBundle>(dc)) return field;

            if (String.IsNullOrEmpty(field.Id))
            {
                field.Id = Guid.NewGuid().ToString();
                field.Status = EntityStatus.Active;
                field.UpdatedTime = DateTime.UtcNow;
            }

            dc.Table<FieldInBundle>().Add(field);
            return field;
        }

        public static Bundle Bundle(this Database dc, String bundleId)
        {
            return dc.Table<Bundle>().Include(x => x.Fields).FirstOrDefault(x => x.Id == bundleId);
        }
    }
}
