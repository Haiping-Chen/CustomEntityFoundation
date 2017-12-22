using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.UnitTest.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.UnitTest.Nodes
{
    [TestClass]
    public class NodeSchemaTest : Database
    {
        [TestMethod]
        public void SchemaTest()
        {
            var bundleEntity = dc.Table<Bundle>().Find(PizzaOrder.BUNDLE_ID_PIZZA_ORDER);
            bundleEntity.Fields = bundleEntity.GetFields(dc);

            Assert.IsTrue(bundleEntity.Fields.Count == 6);
        }
    }
}
