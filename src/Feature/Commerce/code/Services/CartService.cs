using System.Linq;
using SitecoreCoffee.Feature.Commerce.Models;
using SitecoreCoffee.Feature.Commerce.Repositories;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICommerceCartRepository _commerceCartRepository;

        public CartService(ICommerceCartRepository commerceCartRepository)
        {
            _commerceCartRepository = commerceCartRepository;
        }

        public Cart GetCart()
        {
            var cart = _commerceCartRepository.GetCart();
            return MapCommerceCart(cart);
        }

        public static Cart MapCommerceCart(Sitecore.Commerce.Entities.Carts.Cart cart)
        {
            return new Cart()
            {
                Name = cart.Name,
                TotalAmount = cart.Total.Amount,
                Products = cart.Lines.Select(x => new Product()
                {
                    Name = x.Product.ProductName,
                    Price = x.Product.Price.Amount
                }).ToList(),
                IsPopulated = cart.GetPropertyValue("IsPopulated")?.ToString() == "1",

                Info = new CartInternalInfo()
                {
                    ExternalId = cart.ExternalId,
                    ShopName = cart.ShopName,
                    UserId = cart.UserId
                }
            };
        }
    }
}