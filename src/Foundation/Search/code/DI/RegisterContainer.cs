using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Foundation.Search.Services;

namespace SitecoreCoffee.Foundation.Search.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISearchService, SearchService>();
        }
    }
}