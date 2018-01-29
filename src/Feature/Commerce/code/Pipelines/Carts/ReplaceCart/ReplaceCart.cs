using Sitecore.Commerce.Pipelines;
using Sitecore.Diagnostics;
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
            
            replaceCartRequest.ToCart.Properties = replaceCartRequest.FromCart.Properties;
            replaceCartRequest.ToCart.Lines = replaceCartRequest.FromCart.Lines;
            replaceCartRequest.ToCart.Adjustments = replaceCartRequest.FromCart.Adjustments;
            replaceCartRequest.ToCart.Parties = replaceCartRequest.FromCart.Parties;
            replaceCartRequest.ToCart.Payment = replaceCartRequest.FromCart.Payment;
            replaceCartRequest.ToCart.Shipping = replaceCartRequest.FromCart.Shipping;

            replaceCartRequest.ToCart.Total = replaceCartRequest.FromCart.Total;

            replaceCartRequest.ToCart.AccountingCustomerParty = replaceCartRequest.FromCart.AccountingCustomerParty;
            replaceCartRequest.ToCart.BuyerCustomerParty = replaceCartRequest.FromCart.BuyerCustomerParty;
            replaceCartRequest.ToCart.CartType = replaceCartRequest.FromCart.CartType;
            replaceCartRequest.ToCart.CurrencyCode = replaceCartRequest.FromCart.CurrencyCode;
            replaceCartRequest.ToCart.CustomerId = replaceCartRequest.FromCart.CustomerId;
            replaceCartRequest.ToCart.Email = replaceCartRequest.FromCart.Email;
            replaceCartRequest.ToCart.IsLocked = replaceCartRequest.FromCart.IsLocked;
            replaceCartRequest.ToCart.LoyaltyCardID = replaceCartRequest.FromCart.LoyaltyCardID;
            replaceCartRequest.ToCart.Name = replaceCartRequest.FromCart.Name;
            replaceCartRequest.ToCart.ShopName = replaceCartRequest.FromCart.ShopName;
            replaceCartRequest.ToCart.Status = replaceCartRequest.FromCart.Status;

            cartResult.Cart = replaceCartRequest.ToCart;
        }
    }
}