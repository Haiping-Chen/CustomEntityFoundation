using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Nodes;
using CustomEntityFoundation.UnitTest.TestData;
using CustomEntityFoundation.Utilities;
using EntityFrameworkCore.BootKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.UnitTest.Nodes
{
    [TestClass]
    public class NodeQueryTest : Database
    {
        [TestMethod]
        public void QueryTest1()
        {
            var bundle = dc.Bundle.Find(PizzaOrder.BUNDLE_ID_PIZZA_ORDER);
            var record = bundle.QueryRecords(dc).First();
            Assert.IsNotNull(record.Id);
        }
    }
}
