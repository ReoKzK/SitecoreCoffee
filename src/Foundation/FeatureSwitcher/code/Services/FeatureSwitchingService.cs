using System;
using SitecoreCoffee.Foundation.FeatureSwitcher.Models;
using SitecoreCoffee.Foundation.ORM.Repositories;

namespace SitecoreCoffee.Foundation.FeatureSwitcher.Services
{
    public class FeatureSwitchingService : IFeatureSwitchingService
    {
        private readonly IContentRepository _contentRepository;

        public FeatureSwitchingService(
            IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public bool IsEnabled(string featureName)
        {
            return true;
        }

        public bool IsEnabled(Guid switcherId)
        {
            var switcherItem = _contentRepository.GetItem<ISwitcher>(switcherId);
            return switcherItem.Enabled;
        }
    }
}