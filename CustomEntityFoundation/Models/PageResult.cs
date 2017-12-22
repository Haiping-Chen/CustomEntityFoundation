using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.Models
{
    public class PageResult<T>
    {
        public PageResult()
        {
            Page = 1;
            Size = 10;
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
