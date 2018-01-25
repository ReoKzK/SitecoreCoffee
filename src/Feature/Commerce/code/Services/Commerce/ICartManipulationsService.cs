using Sitecore.Commerce.Entities.Carts;

namespace SitecoreCoffee.Feature.Commerce.Services.Commerce
{
    public interface ICartManipulationsService
    {
        Cart MergeCarts(Cart userCart, Cart anonymousCart);

        Cart ReplaceCarts(Cart fromCart, Cart toCart);
    }
}