using System.Web.Mvc;
using SitecoreCoffee.Feature.Commerce.Mediators;

namespace SitecoreCoffee.Feature.Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartMediator _cartMediator;

        public CartController(ICartMediator cartMediator)
        {
            _cartMediator = cartMediator;
        }

        public ActionResult CartListing()
        {
            var mediatorResponse = _cartMediator.GetCart();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.GetCart.Ok:
                    return PartialView(mediatorResponse.ViewModel);

                case MediatorCodes.CartMediator.GetCart.Fail:
                default:
                    return PartialView();
            }
        }
    }
}