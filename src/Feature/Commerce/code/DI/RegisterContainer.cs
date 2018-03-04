using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Contacts;
using Sitecore.DependencyInjection;
using SitecoreCoffee.Feature.Commerce.Mediators;
using SitecoreCoffee.Feature.Commerce.Repositories;
using SitecoreCoffee.Feature.Commerce.Services;
using SitecoreCoffee.Feature.Commerce.Services.CarPartsShop;
using SitecoreCoffee.Feature.Commerce.Services.Commerce;

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
            serviceCollection.AddTransient<ICartManipulationsService, CartManipulationsService>();
            serviceCollection.AddTransient<IContactService, ContactService>();
            serviceCollection.AddTransient<IMapperService, MapperService>();
            serviceCollection.AddTransient<ICarPartsShopSystemService, CarPartsShopSystemService>();

            serviceCollection.AddTransient<ICommerceCartRepository, CommerceCartRepository>();
        }
    }
}