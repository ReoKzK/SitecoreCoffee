using Sitecore.Commerce.Entities.Carts;

namespace SitecoreCoffee.Feature.Commerce.Repositories
{
    public interface ICommerceCartRepository
    {
        string ShopName { get; set; }
        string UserId { get; }

        Cart AddToCart(string productId, uint quantity);
        bool DeleteCart();
        Cart GetCart();
        Cart LockCart();

        Cart EnsureCorrectCartUserId(Cart cart);
    }
}