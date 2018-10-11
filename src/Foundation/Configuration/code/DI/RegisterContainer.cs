using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Foundation.Configuration.Services;

namespace SitecoreCoffee.Foundation.Configuration.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISiteConfigurationService, SiteConfigurationService>();
        }
    }
}