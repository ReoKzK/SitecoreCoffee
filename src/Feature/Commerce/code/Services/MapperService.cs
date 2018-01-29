using System.Linq;
using SitecoreCoffee.Feature.Commerce.Models;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public class MapperService : IMapperService
    {
        public Cart MapCommerceCart(Sitecore.Commerce.Entities.Carts.Cart cart)
        {
            return new Cart()
            {
                Name = cart.Name,
                TotalAmount = cart.Total?.Amount ?? decimal.Zero,
                Products = cart.Lines.Select(x => new Product()
                {
                    Name = x.Product?.ProductName,
                    Id = x.Product?.ProductId,
                    Sku = "",
                    Price = x.Product?.Price?.Amount ?? decimal.Zero
                }).ToList(),

                Properties = new CartCustomProperties
                {
                    IsPopulated = cart.GetPropertyValue("IsPopulated")?.ToString() == "1",
                    VatNumber = cart.GetPropertyValue("VatNumber")?.ToString()
                },

                Info = new CartInternalInfo()
                {
                    ExternalId = cart.ExternalId,
                    ShopName = cart.ShopName,
                    UserId = cart.UserId
                }
            };
        }

        public Cart MapCommerceBaseCart(Sitecore.Commerce.Entities.Carts.CartBase cart)
        {
            return new Cart()
            {
                Name = cart.Name,

                Properties = new CartCustomProperties
                {
                    IsPopulated = cart.GetPropertyValue("IsPopulated")?.ToString() == "1",
                    VatNumber = cart.GetPropertyValue("VatNumber")?.ToString()
                },

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