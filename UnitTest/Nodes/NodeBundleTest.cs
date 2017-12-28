using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;
using CustomEntityFoundation.UnitTest.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    [TestClass]
    public class NodeBundleTest : TestEssential
    {
        [TestMethod]
        public void CreateNodeRecordTest()
        {
            var pizzaType = new PizzaType();

            pizzaType.CreatePizzaTypeBundle();
            pizzaType.CreatePizzaTypeRecord();

            var pizza = new PizzaOrder();

            pizza.CreatePizzaOrderBundle();
            pizza.CreatePizzaOrderRecord();
        }

        [TestMethod]
        public void CheckNodeFieldsTest()
        {
            var bundle = dc.Table<Bundle>().Find(PizzaOrder.BUNDLE_ID_PIZZA_ORDER);
            var fields = bundle.GetFields(dc);

            Assert.IsTrue(fields.Count == 6);
        }
    }
}
