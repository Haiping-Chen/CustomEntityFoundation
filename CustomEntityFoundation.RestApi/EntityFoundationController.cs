using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CustomEntityFoundation.RestApi
{
    [Authorize]
    [Produces("application/json")]
    [Route("cef/[controller]")]
    public abstract class EntityFoundationController : ControllerBase
    {
        protected EntityDbContext dc { get; set; }

        public EntityFoundationController()
        {
            dc = new EntityDbContext();
            dc.InitDb();
        }
    }
}
