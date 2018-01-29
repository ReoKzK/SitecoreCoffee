using Sitecore.Commerce.Entities.Carts;
using SitecoreCoffee.Feature.Commerce.Models;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public interface IMapperService
    {
        Models.Cart MapCommerceCart(Sitecore.Commerce.Entities.Carts.Cart cart);

        Models.Cart MapCommerceBaseCart(Sitecore.Commerce.Entities.Carts.CartBase cart);
    }
}