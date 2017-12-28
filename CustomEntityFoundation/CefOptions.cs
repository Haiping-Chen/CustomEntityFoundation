using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation
{
    public class CefOptions
    {
        /// <summary>
        /// SqlServer, Sqlite, MySql, InMemory
        /// </summary>
        public String Database { get; set; }
        public String ConnectionString { get; set; }

        public static String[] Assembles { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static string ContentRootPath { get; set; }
    }
}
