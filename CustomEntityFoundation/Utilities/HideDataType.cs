using CustomEntityFoundation.Bundles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Utilities
{
    public static class HideDataType
    {
        public static List<BundleDbRecord> HidePassword(this List<BundleDbRecord> records)
        {
            if (records.Count() > 0)
            {
                Type type = records.First().GetType();
                type.GetProperties().ToList().ForEach(property =>
                {
                    var attributeData = property.GetCustomAttributesData().FirstOrDefault(x => x.AttributeType.Name == "DataTypeAttribute");
                    if (attributeData != null)
                    {
                        var attributeValue = (int)attributeData.ConstructorArguments.First().Value;
                        if (attributeValue == (int)DataType.Password)
                        {
                            records.ForEach(r => r.SetValue(property.Name, String.Empty));
                        }
                    }
                });
            }

            return records;
        }
    }
}
