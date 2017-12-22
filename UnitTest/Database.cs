using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    public abstract class Database
    {
        protected EntityDbContext dc { get; set; }

        public Database()
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

            dc = new EntityDbContext();
            dc.InitDb();
        }
    }
}
