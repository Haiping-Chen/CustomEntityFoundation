using EntityFrameworkCore.BootKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.IO;

namespace CustomEntityFoundation.RestApi
{
#if !DEBUG
    [Authorize]
#endif
    [Produces("application/json")]
    [Route("cef/[controller]")]
    public abstract class EntityFoundationController : ControllerBase
    {
        protected Database dc { get; set; }

        public EntityFoundationController()
        {
            dc = new DefaultDataContextLoader().GetDefaultDc();
        }
    }
}
