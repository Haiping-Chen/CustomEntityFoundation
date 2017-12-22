using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    public interface IBundlableEntity
    {
        void LoadEntity(EntityDbContext dc, String entityName);
        bool InsertEntity(EntityDbContext dc, String entityName);

        /// <summary>
        /// Convert to readable business object
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        JObject ToBusinessObject(EntityDbContext dc, String entityName);
    }
}
