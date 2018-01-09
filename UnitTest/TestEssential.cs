using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    public abstract class TestEssential
    {
        protected Database dc { get; set; }

        public TestEssential()
        {
            Database.ContentRootPath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\App_Data";
            Database.Assemblies = new string[] { "CustomEntityFoundation.Core" };

            dc = new Database();

            dc.BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
            {
                MasterConnection = new SqliteConnection($"Data Source={Database.ContentRootPath}\\content.db"),
                CreateDbIfNotExist = true
            });
        }
    }
}
