using EntityFrameworkCore.BootKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Bundles
{
    public interface IBundlableEntity
    {
        void LoadEntity(Database dc, String entityName);
        bool InsertEntity(Database dc, String entityName);

        /// <summary>
        /// Convert to readable business object
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        JObject ToBusinessObject(Database dc, String entityName);
    }
}
