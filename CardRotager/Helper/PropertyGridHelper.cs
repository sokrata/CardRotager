using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace CardRotager {
    public static class PropertyGridHelper {
        public static void setDefaultColor(object selectedObject, string propertyName, Color newDefaultColor) {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(selectedObject);
            AttributeCollection attribs = props[propertyName].Attributes;

            DefaultValueAttribute attr = (DefaultValueAttribute) attribs[typeof(DefaultValueAttribute)];
            // attr = (BrowsableAttribute) attribs[BrowsableAttribute.Default.GetType()];

            Type attrType = attr.GetType();
            FieldInfo fld = attrType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attr, newDefaultColor);
        }
    }
}