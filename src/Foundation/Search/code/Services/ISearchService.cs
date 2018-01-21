using System.Collections.Generic;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace SitecoreCoffee.Foundation.Search.Services
{
    public interface ISearchService
    {
        List<SearchResultItem> Search(string indexName, string content, Language language = null);

        List<Item> SearchItems(string indexName, string content, Language language = null);
    }
}