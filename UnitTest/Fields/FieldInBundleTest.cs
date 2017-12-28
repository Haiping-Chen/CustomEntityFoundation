using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Fields;
using CustomEntityFoundation.UnitTest.TestData;
using CustomEntityFoundation.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    [TestClass]
    public class FieldInBundleTest : TestEssential
    {
        public void AddFieldToBundleTest()
        {
            var input = new { BundleId = PizzaType.BUNDLE_ID_PIZZA_TYPE, FieldTypeName = "Text", Name = "NewField", Caption = "NewField" };
            var bundle = dc.Table<Bundle>().Find(input.BundleId);

            FieldInBundle newField = null;

            var fields1 = bundle.GetFields(dc);

            int row = dc.DbTran(() => {
                newField = bundle.AddField(dc, input.ToObject<FieldInBundle>());
            });

            var fields2 = bundle.GetFields(dc);

            Assert.IsTrue(fields2.Count - fields1.Count == row);
        }
    }
}
