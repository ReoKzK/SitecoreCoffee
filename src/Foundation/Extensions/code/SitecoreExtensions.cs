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
            return Sitecore.Configuration.Factory.GetSiteInfoList()
                .Where(x =>
                    item.Paths.FullPath.StartsWith(x.RootPath, StringComparison.InvariantCultureIgnoreCase)
                    && item.Paths.FullPath.Length >= x.RootPath.Length)
                .OrderByDescending(x => x.RootPath.Length)
                .FirstOrDefault();
        }
    }
}