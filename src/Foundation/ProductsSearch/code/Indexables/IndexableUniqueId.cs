using Sitecore.ContentSearch;
using System;

namespace SitecoreCoffee.Foundation.ProductsSearch.Indexables
{
    public class IndexableUniqueId : IIndexableUniqueId
    {
        private IIndexableId _id;

        private Guid _guid;

        public IndexableUniqueId(IIndexableId id, Guid guid)
        {
            _id = id;
            _guid = guid;
        }

        public IIndexableId GroupId
        {
            get
            {
                return _id;
            }
        }

        public object Value
        {
            get
            {
                return _guid;
            }
        }

        public override string ToString()
        {
            return _id.Value.ToString();
        }
    }
}