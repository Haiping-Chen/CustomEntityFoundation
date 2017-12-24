using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.Fields
{
    public interface IFieldRepository
    {
        List<Object> Extract(String entityId, FieldInBundle field, JToken jo, Type joType);
        Object ToBusinessObject(FieldInBundle field);
    }
}
