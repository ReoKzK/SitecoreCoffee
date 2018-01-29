using SitecoreCoffee.Feature.Commerce.Models;
using System.Collections.Generic;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public interface ICartService
    {
        Cart GetCart();

        Cart AddToCart(string productId, uint quantity = 1);

        Cart IdenfifyContactInCart(string email, bool replaceExistingUserCart = false);

        bool DeleteCart();

        List<Cart> GetCarts(CartSearchModel search);

        Cart CreateNewCart(string cartName = "");

        Cart SetCartProperty(string key, object value);
    }
}