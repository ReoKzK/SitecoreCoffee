using Sitecore.Commerce.Services.Carts;
using SitecoreCoffee.Feature.Commerce.Pipelines.Carts.ReplaceCart;

namespace SitecoreCoffee.Feature.Commerce.Services.Commerce
{
    public class CartServiceProvider : Sitecore.Commerce.Services.Carts.CartServiceProvider
    {
        public virtual CartResult ReplaceCart(ReplaceCartRequest request)
        {
            return this.RunPipeline<ReplaceCartRequest, CartResult>("commerce.carts.replaceCart", request);
        }
    }
}