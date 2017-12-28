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
            CefOptions.ContentRootPath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\App_Data";
            CefOptions.Assembles = new string[] { "CustomEntityFoundation.Core" };

            dc = new Database();

            dc.BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
            {
                MasterConnection = new SqliteConnection($"Data Source={CefOptions.ContentRootPath}\\content.db"),
                CreateDbIfNotExist = true,
                AssemblyNames = CefOptions.Assembles
            });
        }
    }
}
