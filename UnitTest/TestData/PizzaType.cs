using CustomEntityFoundation.Bundles;
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
    public class PizzaType : TestEssential
    {
        public static String BUNDLE_ID_PIZZA_TYPE = "fa623a3a-5e8d-4ad5-a3f0-dff4e48b3750";
        public static String BUNDLE_ID_PIZZA_TYPE_1 = "3e7823f3-7672-42de-b4a1-300192702a02";
        public static String BUNDLE_ID_PIZZA_TYPE_2 = "dc4c6ac5-7200-4df4-900a-cb94a43a9b16";

        /// <summary>
        /// define a pizza type
        /// </summary>
        public void CreatePizzaTypeBundle()
        {
            Bundle bundle = new Bundle
            {
                Name = $"Pizza Types",
                EntityName = "Node",
                Id = BUNDLE_ID_PIZZA_TYPE,
                Fields = new List<FieldInBundle>()
            };

            bundle.Fields.AddRange(new List<FieldInBundle>{
                new FieldInBundle
                {
                    FieldTypeName = "File",
                    Name = "Photo",
                    Caption = "Photo",
                    IsMultiple = true
                },

                new FieldInBundle
                {
                    FieldTypeName = "Boolean",
                    Name = "Sale",
                    Caption = "Sale"
                }
            });

            if (!bundle.IsExist<Bundle>(dc))
            {
                int rows = dc.DbTran(() => dc.Table<Bundle>().Add(bundle));
            }
        }

        /// <summary>
        /// Create pizza types
        /// </summary>
        public void CreatePizzaTypeRecord()
        {
            var record1 = JObject.FromObject(new
            {
                Id = BUNDLE_ID_PIZZA_TYPE_1,
                Name = "Neapolitan",
                Description = "The Neapolitan pizza is the original pizza that left Italy and arrived with Italian immigrants in the United States.",
                Sale = false,
                Photo = new List<Object> {
                    new
                    {
                        Title = "Neapolitan1",
                        Size = 17001,
                        Path = "https://www.shefinds.com/files/2015/09/pizza.jpg"
                    },
                    new
                    {
                        Title = "Neapolitan2",
                        Size = 17002,
                        Path = "https://www.shefinds.com/files/2015/09/pizza.jpg"
                    }
                }
            });

            var record2 = JObject.FromObject(new
            {
                Id = BUNDLE_ID_PIZZA_TYPE_2,
                Name = "Chicago Deep Dish",
                Description = "In the 1940s, Pizzeria Uno in Chicago developed the deep-dish pizza, which has a deep crust that lines a deep dish, similar to a large metal cake or pie pan.",
                Sale = true,
                Photo = new List<Object> {
                    new
                    {
                        Title = "Chicago Deep Dish1",
                        Size = 18001,
                        Path = "http://www.cafechococraze.com/wp-content/uploads/2016/06/paneer-el-panso.jpg"
                    },
                    new
                    {
                        Title = "Chicago Deep Dish2",
                        Size = 18002,
                        Path = "http://www.cafechococraze.com/wp-content/uploads/2016/06/paneer-el-panso.jpg"
                    }
                }
            });

            var bundle = dc.Table<Bundle>().Include(x => x.Fields).First(x => x.Id == BUNDLE_ID_PIZZA_TYPE);

            BundleDbRecord node1 = null;
            BundleDbRecord node2 = null;

            int rows = dc.DbTran(() =>
            {
                node1 = bundle.AddRecord(dc, record1);
                node2 = bundle.AddRecord(dc, record2);
            });

            var loadedNode1 = dc.Table<Bundle>().Find(node1.BundleId).LoadRecord(dc, node1.Id);
            Assert.IsTrue(loadedNode1.Name == record1["Name"].ToString());

            var bo1 = loadedNode1.ToBusinessObject(dc, bundle.EntityName);
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(bo1["Photo"][i]["Title"].ToString() == record1["Photo"][i]["Title"].ToString());
                Assert.IsTrue(bo1["Photo"][i]["Size"].ToString() == record1["Photo"][i]["Size"].ToString());
                Assert.IsTrue(bo1["Photo"][i]["Size"].ToString() == record1["Photo"][i]["Size"].ToString());
            }
            Assert.IsTrue(bo1["Sale"].ToString() == record1["Sale"].ToString());

            var loadedNode2 = dc.Table<Bundle>().Find(node2.BundleId).LoadRecord(dc, node2.Id);
            Assert.IsTrue(loadedNode2.Name == record2["Name"].ToString());
        }
    }
}
