using CustomEntityFoundation.Fields;
using CustomEntityFoundation.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.UnitTest
{
    [TestClass]
    public class FieldTypeTest : TestEssential
    {
        [TestMethod]
        public void FieldTypesTest()
        {
            var fieldTypeNames = FieldRepository.FieldTypeNames();
        }
    }
}
