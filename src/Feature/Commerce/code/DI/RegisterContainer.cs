using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Contacts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.DependencyInjection;

namespace SitecoreCoffee.Feature.Commerce.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CartServiceProvider>();
            serviceCollection.AddTransient<ContactFactory>();
        }
    }
}