using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreCoffee.Foundation.Search.Services
{
    public class SearchService
    {
        public List<Item> SearchItems(String indexName, String content, Language language = null)
        {
            var resultItems = new List<Item>();

            if (language == null)
            {
                language = Sitecore.Context.Language;
            }

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
            var resultItems = new List<SearchResultItem>();

            if (language == null)
            {
                language = Sitecore.Context.Language;
            }

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