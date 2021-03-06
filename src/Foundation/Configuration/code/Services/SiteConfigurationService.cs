﻿using SitecoreCoffee.Foundation.ORM.Repositories;

namespace SitecoreCoffee.Foundation.Configuration.Services
{
    public class SiteConfigurationService : ISiteConfigurationService
    {
        public static string SiteSettingsQuery => 
            $"./ancestor-or-self::*[@@templateid='{Constants.RootPageTemplateId.ToString("B").ToUpper()}']/*[@@templateid='{Constants.SiteSettingsTemplateId.ToString("B").ToUpper()}']";

        private readonly IContentRepository _contentRepository;

        public SiteConfigurationService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public T GetConfiguration<T>() where T : class
        {
            T item = null;

            if (Sitecore.Context.Item != null)
            {
                item = _contentRepository.GetRelativeItem<T>(SiteSettingsQuery);
            }
            
            return item;
        }
    }
}