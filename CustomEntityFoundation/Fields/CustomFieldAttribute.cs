using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    public class EntityPropertyAsField : Attribute
    {
        private String fieldTypeName;
        private String fieldName;

        public EntityPropertyAsField(String fieldName, String fieldTypeName)
        {
            this.fieldTypeName = fieldTypeName;
            this.fieldName = fieldName;
        }

        public FieldInBundle GetBundleField()
        {
            return new FieldInBundle
            {
                FieldTypeName = fieldTypeName,
                Name = fieldName
            };
        }
    }
}
