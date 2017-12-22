using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    public class FieldTypeAttribute : Attribute
    {
        private String fieldTypeName;

        public FieldTypeAttribute(String fieldTypeName)
        {
            this.fieldTypeName = fieldTypeName;
        }

        public String GetFieldTypeName()
        {
            return fieldTypeName;
        }
    }
}
