using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomEntityFoundation.Utilities;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Bundles;

namespace CustomEntityFoundation.RestApi.Bundles
{
    public class BundleController : EntityFoundationController
    {
        /// <summary>
        /// Get all bundles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Object> GetBundles()
        {
            return dc.Table<Bundle>().Where(x => x.Status == EntityStatus.Active)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    x.EntityName
                });
        }

        /// <summary>
        /// Get Business Object Schema
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        [HttpGet("{bundleId}")]
        public IActionResult GetBundleSchema([FromRoute] string bundleId)
        {
            var bundleEntity = dc.Bundle.Find(bundleId);

            if (bundleEntity == null)
            {
                return NotFound();
            }

            bundleEntity.Fields = bundleEntity.GetFields(dc);

            return Ok(bundleEntity);
        }

        /// <summary>
        /// Get all base entities
        /// </summary>
        /// <returns></returns>
        [HttpGet("entity")]
        public IEnumerable<String> GetBundlableEntities()
        {
            List<String> bundlableEntities = new List<string>();

            List<Type> core = TypeHelper.GetClassesWithInterface<IBundlableEntity>(EntityDbContext.Assembles);
            core.ForEach(type => {
                bundlableEntities.Add(type.Name.Replace("Entity", ""));
            });

            return bundlableEntities.OrderBy(x => x);
        }

        /// <summary>
        /// Get Business Object Data
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet("{bundleId}/{entityId}")]
        public async Task<IActionResult> GetBundleRecord([FromRoute] string bundleId, [FromRoute] string entityId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bundleEntity = await dc.Table<Bundle>().Include(x => x.Fields).SingleOrDefaultAsync(m => m.Id == bundleId);

            if (bundleEntity == null)
            {
                return NotFound();
            }

            var record = bundleEntity.LoadRecord(dc, entityId);

            return Ok(record);
        }
    }
}