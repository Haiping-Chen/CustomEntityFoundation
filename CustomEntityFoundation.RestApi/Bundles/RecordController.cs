using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Models;
using CustomEntityFoundation.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.RestApi.Bundles
{
    public class RecordController : EntityFoundationController
    {
        [HttpGet("{bundleId}")]
        public PageResult<BundleDbRecord> GetRecordsInBundle([FromRoute] String bundleId, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var bundle = dc.Table<Bundle>().Find(bundleId);

            var query = bundle.QueryRecords(dc);

            var total = query.Count();
            var items = query.Skip((page - 1) * size).Take(size).ToList();

            items.HidePassword();

            return new PageResult<BundleDbRecord> { Total = total, Page = page, Size = size, Items = items };
        }

        /// <summary>
        /// Save a business instance
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost("{bundleId}")]
        public String SaveRecordInBundle([FromRoute] String bundleId, [FromBody] JObject record)
        {
            var bundle = dc.Table<Bundle>().Include(x => x.Fields).First(x => x.Id == bundleId);

            BundleDbRecord node = null;

            int rows = dc.DbTran(() =>
            {
                node = bundle.AddRecord(dc, record);
            });

            return node.Id;
        }
    }
}
