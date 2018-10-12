using System.Collections.Generic;
using SitecoreCoffee.Foundation.Search.Services;
using System.Web.Mvc;
using SitecoreCoffee.Feature.Search.Models;

namespace SitecoreCoffee.Feature.Search.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult SearchComponent(string search = "")
        {
            var items = new List<SearchItem>(); 
           
            var results = _searchService.SearchItems("search_index", search);

            foreach (var item in results)
            {
                items.Add(new SearchItem
                {
                    Title = item.Name,
                    Url = ""
                });
            }

            return View(items);
        }
    }
}