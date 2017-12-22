using CustomEntityFoundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CustomEntityFoundation.UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void EntityDbContextTest()
        {
            EntityDbContext.Assembles = new String[] { "CustomEntityFoundation" };
            var options = new DatabaseOptions
            {
                ContentRootPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..",
            };

            // Sqlite
            options.Database = "Sqlite";
            options.ConnectionString = $"Data Source={options.ContentRootPath}\\cef.db";
            EntityDbContext.Options = options;

            var dc = new EntityDbContext();
            dc.InitDb();

            // SQL Server
            options.Database = "SqlServer";
            options.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cef;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            dc = new EntityDbContext();
            dc.InitDb();
        }
    }
}
