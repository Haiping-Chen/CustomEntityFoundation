using CustomEntityFoundation.Bundles;
using EntityFrameworkCore.BootKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    [TestClass]
    public class BundleTest : TestEssential
    {
        [TestMethod]
        public void CreateBundelTest()
        {
            String Id = Guid.NewGuid().ToString();

            Bundle bundle = new Bundle
            {
                Name = $"Pizza Order {DateTime.Now.ToString()}",
                Description = "This is a business data structure as a Pizza Order repository",
                EntityName = "Node",
                Id = Id
            };

            int effected = dc.DbTran(() => dc.Table<Bundle>().Add(bundle));

            Assert.IsTrue(effected == 1);
        }
    }
}
