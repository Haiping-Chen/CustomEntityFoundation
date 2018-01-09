using CustomEntityFoundation.Views;
using DotNetToolkit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.UnitTest.Views
{
    [TestClass]
    public class UserViewTest : TestEssential
    {
        public void LoadRecordsTest()
        {
            var view = new View { Id = "7497d0e2-05eb-4bad-8c23-875a34bac102" }.LoadDefinition(dc);
            view.Result = new PageResult<Object> { Page = 1, Size = 10, Items = new List<Object>() };
            view.LoadRecords(dc);
        }
    }
}
