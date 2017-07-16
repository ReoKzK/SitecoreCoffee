using Sitecore.ContentSearch;
using System;
using System.Reflection;

namespace SitecoreCoffee.Foundation.ProductsSearch.Indexables
{
    public class IndexableDataField : IIndexableDataField
    {
        private object _concreteObject;

        private PropertyInfo _fieldInfo;

        public IndexableDataField(object concreteObject, PropertyInfo fieldInfo)
        {
            _concreteObject = concreteObject;
            _fieldInfo = fieldInfo;
        }

        public Type FieldType
        {
            get
            {
                return _fieldInfo.PropertyType;
            }
        }

        public object Id
        {
            get
            {
                return _fieldInfo.Name.ToLower();
            }
        }

        public String Name
        {
            get
            {
                return _fieldInfo.Name;
            }
        }

        public String TypeKey
        {
            get
            {
                return "string";
            }
        }

        public object Value
        {
            get
            {
                return _fieldInfo.GetValue(_concreteObject);
            }
        }
    }
}