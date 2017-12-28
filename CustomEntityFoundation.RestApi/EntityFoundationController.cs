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
            dc = new Database();

            string db = CefOptions.Configuration.GetSection("Database:Default").Value;
            string connectionString = CefOptions.Configuration.GetSection("Database:ConnectionStrings")[db];

            if (db.Equals("SqlServer"))
            {
                dc.BindDbContext<IDbRecord, DbContext4SqlServer>(new DatabaseBind
                {
                    MasterConnection = new SqlConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("Sqlite"))
            {
                connectionString = connectionString.Replace("|DataDirectory|\\", CefOptions.ContentRootPath + "\\App_Data\\");
                dc.BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
                {
                    MasterConnection = new SqliteConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("MySql"))
            {
                dc.BindDbContext<IDbRecord, DbContext4MySql>(new DatabaseBind
                {
                    MasterConnection = new MySqlConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("InMemory"))
            {
                dc.BindDbContext<IDbRecord, DbContext4Memory>(new DatabaseBind
                {
                    AssemblyNames = CefOptions.Assembles
                });
            }
        }
    }
}
