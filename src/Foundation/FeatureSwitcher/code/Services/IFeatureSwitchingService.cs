using System;

namespace SitecoreCoffee.Foundation.FeatureSwitcher.Services
{
    public interface IFeatureSwitchingService
    {
        bool IsEnabled(Guid switcherId);
        bool IsEnabled(string featureName);
    }
}