using System;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace SitecoreCoffee.Foundation.Extensions
{
    public static class SitecoreExtensions
    {
        /// <summary>
        /// Finds the best match for specified item's site
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <returns>Sitecore SiteInfo</returns>
        public static SiteInfo GetSite(this Item item)
        {
            // - Basing on path, item can fall into a couple of sites as some of them have high-level root like /sitecore/content/
            // - Here we check if item path starts with site root path AND we sort those sites by root path lenght descending
            // - That gives us the best match.
            return Sitecore.Configuration.Factory.GetSiteInfoList()
                .Where(x =>
                    item.Paths.FullPath.StartsWith(x.RootPath, StringComparison.InvariantCultureIgnoreCase)
                    && item.Paths.FullPath.Length >= x.RootPath.Length)
                .OrderByDescending(x => x.RootPath.Length)
                .FirstOrDefault();
        }
    }
}