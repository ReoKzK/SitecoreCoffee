using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreCoffee.Foundation.Search.Services
{
    public class SearchService : ISearchService
    {
        public List<Item> SearchItems(String indexName, String content, Language language = null)
        {
            List<Item> resultItems;

            language = language ?? Sitecore.Context.Language;

            var index = ContentSearchManager.GetIndex(indexName);

            using (var context = index.CreateSearchContext())
            {
                var searchQuery = context.GetQueryable<SearchResultItem>()
                    .Where(x => x.Language.Equals(language.Name))
                    .Where(x => x.Content.Contains(content));

                resultItems = searchQuery
                    .Select(x => x.GetItem())
                    .ToList();
            }

            return resultItems;
        }

        public List<SearchResultItem> Search(String indexName, String content, Language language = null)
        {
            List<SearchResultItem> resultItems;
            
            language = language ?? Sitecore.Context.Language;

            var index = ContentSearchManager.GetIndex(indexName);

            using (var context = index.CreateSearchContext())
            {
                var searchQuery = context.GetQueryable<SearchResultItem>()
                    .Where(x => x.Language.Equals(language.Name))
                    .Where(x => x.Content.Contains(content));

                resultItems = searchQuery.ToList();
            }

            return resultItems;
        }
    }
}