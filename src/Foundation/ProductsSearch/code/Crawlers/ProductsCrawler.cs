using Sitecore.ContentSearch;
using SitecoreCoffee.Foundation.ProductsSearch.Indexables;
using SitecoreCoffee.Foundation.ProductsSearch.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreCoffee.Foundation.ProductsSearch.Crawlers
{
    public class ProductsCrawler : FlatDataCrawler<IndexableProduct>
    {
        protected override IEnumerable<IndexableProduct> GetItemsToIndex()
        {
            var repository = new ProductsRepository();

            return repository.GetProducts()
                             .Select(x => new IndexableProduct(x));
        }

        protected override IndexableProduct GetIndexable(IIndexableUniqueId indexableUniqueId)
        {
            var repository = new ProductsRepository();
            var product = repository.GetProduct((String)indexableUniqueId.GroupId.Value);

            if (product != null)
            {
                return new IndexableProduct(product);
            }

            else
            {
                return null;
            }
        }

        protected override IndexableProduct GetIndexableAndCheckDeletes(IIndexableUniqueId indexableUniqueId)
        {
            //Get the indexable item from the database and check the version
            //Return the indexable item if the version matches what is in the unique id, if not return null
            return null;
        }

        protected override IEnumerable<IIndexableUniqueId> GetIndexablesToUpdateOnDelete(IIndexableUniqueId indexableUniqueId)
        {
            //Return the latest version of the item otherwise return null
            return null;
        }

        protected override bool IndexUpdateNeedDelete(IndexableProduct indexable)
        {
            // Set to false like in SitecoreItemCrawler
            return false;
        }
    }
}