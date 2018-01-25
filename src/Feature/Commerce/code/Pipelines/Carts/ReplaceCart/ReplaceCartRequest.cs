using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;

namespace SitecoreCoffee.Feature.Commerce.Pipelines.Carts.ReplaceCart
{
    public class ReplaceCartRequest : CartRequest
    {
        public Cart FromCart { get; protected set; }

        public Cart ToCart { get; protected set; }

        public ReplaceCartRequest(Cart fromCart, Cart toCart)
        {
            Assert.ArgumentNotNull(fromCart, "fromCart");
            Assert.ArgumentNotNull(toCart, "toCart");

            this.ToCart = toCart;
            this.FromCart = fromCart;
        }
    }
}