using SitecoreCoffee.Feature.Commerce.Models;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public interface ICartService
    {
        Cart GetCart();

        Cart AddToCart(string productId, uint quantity = 1);

        Cart IdenfifyContactInCart(string email, bool replaceExistingUserCart = false);
    }
}