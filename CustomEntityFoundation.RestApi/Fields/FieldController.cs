using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;

namespace CustomEntityFoundation.RestApi.Fields
{
    public class FieldController : EntityFoundationController
    {
        /// <summary>
        /// Get all field types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<String> GetFieldTypes()
        {
            return FieldRepository.FieldTypeNames();
        }
    }
}