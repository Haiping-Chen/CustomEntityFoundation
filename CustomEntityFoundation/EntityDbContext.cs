using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CustomEntityFoundation
{
    public partial class EntityDbContext : Database
    {
        public static String[] Assembles { get; set; }
        public static DatabaseOptions Options { get; set; }
        public static IConfiguration Configuration { get; set; }
        public String TransactionId { get; set; }

        public virtual void InitDb()
        {
            TransactionId = Guid.NewGuid().ToString();

            string db = Options.Database;
            if (db.Equals("SqlServer"))
            {
                BindDbContext<IDbRecord, DbContext4SqlServer>(new DatabaseBind
                {
                    MasterConnection = new SqlConnection(Options.ConnectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = Assembles
                });
            }
            else if (db.Equals("Sqlite"))
            {
                Options.ConnectionString = Options.ConnectionString.Replace("|DataDirectory|\\", Options.ContentRootPath + "\\App_Data\\");
                BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
                {
                    MasterConnection = new SqliteConnection(Options.ConnectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = Assembles
                });
            }
            else if (db.Equals("MySql"))
            {
                BindDbContext<IDbRecord, DbContext4MySql>(new DatabaseBind
                {
                    MasterConnection = new MySqlConnection(Options.ConnectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = Assembles
                });
            }
            else if (db.Equals("InMemory"))
            {
                BindDbContext<IDbRecord, DbContext4Memory>(new DatabaseBind
                {
                    AssemblyNames = Assembles
                });
            }
        }
    }
}
