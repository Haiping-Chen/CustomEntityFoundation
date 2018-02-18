using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.UnitTest.TestData
{
    public class PizzaOrder : TestEssential
    {
        public static String BUNDLE_ID_PIZZA_ORDER = "81e39849-8cde-49f3-8e70-904781d1fd7f";

        /// <summary>
        /// define a pizza order business type
        /// </summary>
        public void CreatePizzaOrderBundle()
        {
            Bundle bundle = new Bundle
            {
                Name = $"Pizza Order",
                Description = "This is a business data structure as a Pizza Order repository",
                EntityName = "Node",
                Id = BUNDLE_ID_PIZZA_ORDER,
                Fields = new List<FieldInBundle>()
            };

            bundle.Fields.AddRange(new List<FieldInBundle>{
                new FieldInBundle
                {
                    FieldTypeName = "Text",
                    Name = "CustomerName",
                    Caption = "Customer Name",
                    Description = "For customer name displayed on ticket"
                },

                new FieldInBundle
                {
                    FieldTypeName = "EntityReference",
                    Name = "PizzaType",
                    Caption = "Pizza Type",
                    RefBundleId = PizzaOrder.BUNDLE_ID_PIZZA_ORDER,
                    IsMultiple = true
                },

                new FieldInBundle
                {
                    FieldTypeName = "DateTime",
                    Name = "DueDate",
                    Caption = "Due Date"
                },

                new FieldInBundle
                {
                    FieldTypeName = "Integer",
                    Name = "Amount",
                    Caption = "Amount"
                }
            });

            if (!bundle.IsExist<Bundle>(dc))
            {
                int rows = dc.DbTran(() => dc.Table<Bundle>().Add(bundle));
            }
        }

        /// <summary>
        /// Create a fake pizza order
        /// </summary>
        public void CreatePizzaOrderRecord()
        {
            int number = new Random().Next(10);

            var record = JObject.FromObject(new
            {
                Name = $"Order #{number}",
                Description = "No description",
                CustomerName = $"Haiping {DateTime.UtcNow.Year} {number}",
                PizzaType = new List<String>
                {
                    number < 5 ? PizzaType.BUNDLE_ID_PIZZA_TYPE_1 : PizzaType.BUNDLE_ID_PIZZA_TYPE_2,
                    number < 5 ? PizzaType.BUNDLE_ID_PIZZA_TYPE_1 : PizzaType.BUNDLE_ID_PIZZA_TYPE_2
                },
                DueDate = DateTime.UtcNow.AddDays(3),
                Amount = 3
            });

            var bundle = dc.Table<Bundle>().Include(x => x.Fields).First(x => x.Id == BUNDLE_ID_PIZZA_ORDER);

            BundleDbRecord node = null;

            int rows = dc.DbTran(() =>
            {
                node = bundle.AddRecord(dc, record);
            });

            var loadedNode = dc.Table<Bundle>().Find(node.BundleId).LoadRecord(dc, node.Id);

            Assert.IsTrue(loadedNode.UpdatedTime.Date == DateTime.UtcNow.Date);
            Assert.IsTrue(loadedNode.Name == record["Name"].ToString());

            var bo = loadedNode.ToBusinessObject(dc, bundle.EntityName);

            Assert.IsTrue(bo["Name"].ToString() == record["Name"].ToString());
            Assert.IsTrue(bo["Description"].ToString() == record["Description"].ToString());
            Assert.IsTrue(bo["CustomerName"].ToString() == record["CustomerName"].ToString());
            for(int i = 0; i< 2; i++)
            {
                Assert.IsTrue(bo["PizzaType"][i].ToString() == record["PizzaType"][i].ToString());
            }
            Assert.IsTrue(bo["DueDate"].ToString() == record["DueDate"].ToString());
            Assert.IsTrue(bo["Amount"].ToString() == record["Amount"].ToString());
        }
    }
}
