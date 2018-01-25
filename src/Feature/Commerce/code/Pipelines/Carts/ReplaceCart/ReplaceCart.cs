using Sitecore.Commerce;
using Sitecore.Commerce.Pipelines;
using Sitecore.Diagnostics;
using System.Collections.ObjectModel;
using Sitecore.Commerce.Services.Carts;
using CartServiceProvider = SitecoreCoffee.Feature.Commerce.Services.Commerce.CartServiceProvider;

namespace SitecoreCoffee.Feature.Commerce.Pipelines.Carts.ReplaceCart
{
    public class ReplaceCart : PipelineProcessor<ServicePipelineArgs>
    {
        public CartServiceProvider CartServiceProvider { get; protected set; }

        public ReplaceCart(CartServiceProvider cartServiceProvider)
        {
            Assert.IsNotNull(cartServiceProvider, "cartServiceProvider");
            this.CartServiceProvider = cartServiceProvider;
        }

        public override void Process(ServicePipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            ReplaceCartRequest replaceCartRequest = (ReplaceCartRequest)args.Request;
            CartResult cartResult = (CartResult)args.Result;

            Assert.IsNotNull(replaceCartRequest, "request");
            Assert.IsNotNull(cartResult, "result");

            if (replaceCartRequest.FromCart.Lines.Count > 0)
            {
                var lines = replaceCartRequest.FromCart.Lines;
                replaceCartRequest.ToCart.Lines = new ReadOnlyCollection<Sitecore.Commerce.Entities.Carts.CartLine>(lines);
            }

            replaceCartRequest.ToCart.Properties = new PropertyCollection();

            foreach (var property in replaceCartRequest.FromCart.Properties)
            {
                replaceCartRequest.ToCart.Properties.Add(property);
            }
            
            cartResult.Cart = replaceCartRequest.ToCart;
        }
    }
}