using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace CardRotager {
    public class PropertyObject : CollectionBase, ICustomTypeDescriptor {
        private Dictionary<string,DynProperty> props;
        private Settings settings;

        public PropertyObject(Settings settings) {
            props = new Dictionary<string, DynProperty>();
            this.settings = settings;
        }

        /// <summary>
        /// Добавим DynProperty в список
        /// </summary>
        /// <param name="value">объект с атрибутами</param>
        public void Add(DynProperty value) {
            props.Add(value.Name, value);
        }

        /// <summary>
        /// Удалим свойство из списка по имен
        /// </summary>
        /// <param name="name">имя</param>
        public void Remove(string name) {
                    props.Remove(name);
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        public DynProperty this[string name] => props[name];

        public bool ContainProperty(string name) {
            return props.ContainsKey(name);
        }

        #region "TypeDescriptor Implementation"

        public string GetClassName() {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes() {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetComponentName() {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter() {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent() {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty() {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType) {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes) {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents() {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
            PropertyDescriptor[] newProps = new PropertyDescriptor[props.Count];
            int i = 0;
            foreach (KeyValuePair<string, DynProperty> keyValuePair in props) {
                DynProperty prop = keyValuePair.Value;
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
                i++;
            }

            return new PropertyDescriptorCollection(newProps);
        }

        public PropertyDescriptorCollection GetProperties() {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd) {
            return this;
        }

        #endregion

        public void addParam(string category, string name, object defaultValue, string description, bool browsable = true, bool readOnly = false, object editor = null) {
            Add(new DynProperty(name, defaultValue, false, true, description, category, editor));
        }

        public new IEnumerator GetEnumerator() {
            return props.Values.GetEnumerator();
        }
    }

    public class DynProperty {
        public string Category { get; }
        public string Name { get; }
        public object Value { get; set; }
        public object DefaultValue { get; }
        public string Description { get; }
        public bool Visible { get; }
        public bool ReadOnly { get; }
        public object Editor { get; }

        public string StringValue {
            get {
                string result = null;
                if (Value == null) {
                    return result;
                }
                Type memberInfo = Value.GetType();
                if (memberInfo == typeof(Color)) {
                    return Util.ToHtml((Color) Value);
                }
                if (memberInfo == typeof(bool)) {
                    return Value.ToString().ToLower();
                }
                return Value.ToString();
            }
            set {
                if (value == null) {
                    Value = null;
                    return;
                }
                Type type = Value.GetType();
                if (type == typeof(string)) {
                    Value = value;
                } else if (type == typeof(Color)) {
                    Value = ColorTranslator.FromHtml(value);
                } else if (type == typeof(bool) && bool.TryParse(value, out bool cb)) {
                    Value = cb;
                } else if (type == typeof(int) && int.TryParse(value, out int intValue)) {
                    Value = intValue;
                } else if (type == typeof(long) && long.TryParse(value, out long longValue)) {
                    Value = longValue;
                }
            }
        }

        public DynProperty(string sName, object defaultValue, bool bReadOnly, bool bVisible, string description, string category, object editor = null) {
            Value = defaultValue;
            Name = sName;
            DefaultValue = defaultValue;
            ReadOnly = bReadOnly;
            Visible = bVisible;
            Description = description;
            Category = category;
            Editor = editor;
        }
    }

    public class CustomPropertyDescriptor : PropertyDescriptor {
        DynProperty prop;

        public CustomPropertyDescriptor(ref DynProperty myProperty, Attribute[] attrs) : base(myProperty.Name, attrs) {
            prop = myProperty;
        }

        #region PropertyDescriptor specific

        public override object GetEditor(Type editorBaseType) {
            if (prop.Editor == null) {
                return base.GetEditor(editorBaseType);
            }
            return prop.Editor;
        }

        public override bool CanResetValue(object component) {
            return prop.DefaultValue != null;
        }

        public override Type ComponentType => null;

        public override object GetValue(object component) {
            return prop.Value;
        }

        public override void SetValue(object component, object value) {
            prop.Value = value;
        }

        public override string Description => prop.Description;
        public override string Category => prop.Category;
        public override string DisplayName => prop.Name;
        public override bool IsReadOnly => prop.ReadOnly;

        public override void ResetValue(object component) {
            if (prop.DefaultValue != null) {
                prop.Value = prop.DefaultValue;
            }
        }

        public override bool ShouldSerializeValue(object component) {
            return !Equals(prop.Value, prop.DefaultValue);
        }

        public override Type PropertyType => prop.Value.GetType();

        #endregion
    }
}