using System.Collections.Generic;
using Sitecore.Commerce.Entities.Carts;
using SitecoreCoffee.Feature.Commerce.Models;
using Cart = Sitecore.Commerce.Entities.Carts.Cart;

namespace SitecoreCoffee.Feature.Commerce.Repositories
{
    public interface ICommerceCartRepository
    {
        string ShopName { get; set; }
        string UserId { get; }

        Cart AddToCart(string productId, uint quantity);

        bool DeleteCart();

        List<CartBase> GetCarts(CartSearchModel search);

        Cart GetCart();

        Cart LockCart();

        void SetCartProperty(string key, object value);

        Cart EnsureCorrectCartUserId(Cart cart);
    }
}