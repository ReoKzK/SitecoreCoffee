using Sitecore.ContentSearch;
using SitecoreCoffee.Foundation.ProductsSearch.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SitecoreCoffee.Foundation.ProductsSearch.Indexables
{
    public class IndexableProduct : IIndexable
    {
        private Product _product;

        private IEnumerable<IndexableDataField> _fields;

        public IndexableProduct(Product product)
        {
            _product = product;
            _fields = _product.GetType()
                              .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                              .Select(x => new IndexableDataField(_product, x));
        }

        public String AbsolutePath
        {
            get
            {
                return "/";
            }
        }

        public CultureInfo Culture
        {
            get
            {
                return CultureInfo.InvariantCulture;
            }
        }

        public IEnumerable<IIndexableDataField> Fields
        {
            get
            {
                return _fields;
            }
        }

        public IIndexableDataField GetFieldById(object fieldId)
        {
            return _fields.FirstOrDefault(x => x.Id == fieldId);
        }

        public IIndexableDataField GetFieldByName(String fieldName)
        {
            return _fields.FirstOrDefault(x => x.Name.ToLower() == fieldName.ToLower());
        }

        public IIndexableId Id
        {
            get
            {
                return new IndexableId<String>(_product.Id);
            }
        }

        public void LoadAllFields()
        {
            _fields = _product.GetType()
                              .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                              .Select(x => new IndexableDataField(_product, x));
        }

        public IIndexableUniqueId UniqueId
        {
            get
            {
                return new IndexableUniqueId(Id, Guid.NewGuid());
            }
        }

        public String DataSource
        {
            get
            {
                return "Products";
            }
        }
    }
}