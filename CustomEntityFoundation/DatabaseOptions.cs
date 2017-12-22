using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation
{
    public class DatabaseOptions
    {
        /// <summary>
        /// SqlServer, Sqlite, MySql, InMemory
        /// </summary>
        public String Database { get; set; }
        public String ConnectionString { get; set; }
        public String ContentRootPath { get; set; }
    }
}
