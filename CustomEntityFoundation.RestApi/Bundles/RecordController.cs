﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.RestApi.Bundles
{
    public class RecordController : EntityFoundationController
    {
        [HttpGet("{bundleId}")]
        public IEnumerable<Object> GetBundles([FromRoute] String bundleId)
        {
            var bundle = dc.Bundle.Find(bundleId);

            return bundle.QueryRecords(dc);
        }
    }
}
