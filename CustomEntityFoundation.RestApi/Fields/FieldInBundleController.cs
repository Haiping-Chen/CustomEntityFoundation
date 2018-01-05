using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;

namespace CustomEntityFoundation.RestApi.Fields
{
    public class FieldInBundleController : EntityFoundationController
    {
        /// <summary>
        /// Get field detail in a bundle
        /// </summary>
        /// <param name="fieldIdInBundle"></param>
        /// <returns></returns>
        [HttpGet("{fieldIdInBundle}")]
        public FieldInBundle Get([FromRoute] String fieldIdInBundle)
        {
            return dc.Table<FieldInBundle>().Find(fieldIdInBundle);
        }

        /// <summary>
        /// Add new field to business object schema
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        [HttpPost]
        public string Add(FieldInBundle field)
        {
            var bundle = dc.Table<Bundle>().Find(field.BundleId);
            string id = String.Empty;

            dc.DbTran(() => {
                var fieldInBundle = bundle.AddField(dc, field);
                id = fieldInBundle.Id;
            });

            return id;
        }

        /// <summary>
        /// remove field from business object schema
        /// </summary>
        /// <param name="fieldIdInBundle"></param>
        /// <returns></returns>
        [HttpDelete("{fieldIdInBundle}")]
        public bool Delete(string fieldIdInBundle)
        {
            dc.DbTran(() => {
                var fieldInBundle = dc.Table<FieldInBundle>().Find(fieldIdInBundle);
                dc.Table<FieldInBundle>().Remove(fieldInBundle);
            });

            return true;
        }

        /// <summary>
        /// Update field setting in a bundle
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        [HttpPut("{fieldIdInBundle}")]
        public bool Update(FieldInBundle field)
        {
            return true;
        }
    }
}