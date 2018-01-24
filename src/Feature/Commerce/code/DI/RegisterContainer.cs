using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Contacts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Feature.Commerce.Mediators;
using SitecoreCoffee.Feature.Commerce.Repositories;
using SitecoreCoffee.Feature.Commerce.Services;

namespace SitecoreCoffee.Feature.Commerce.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CartServiceProvider>();
            serviceCollection.AddTransient<ContactFactory>();

            serviceCollection.AddTransient<ICartMediator, CartMediator>();
            serviceCollection.AddTransient<ICartService, CartService>();
            serviceCollection.AddTransient<ICommerceCartRepository, CommerceCartRepository>();
        }
    }
}