using System.Web.Mvc;
using SitecoreCoffee.Feature.Commerce.Mediators;
using SitecoreCoffee.Feature.Commerce.ViewModel;

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

        [HttpPost]
        public ActionResult AddToCart(CartAddViewModel viewmodel)
        {
            var mediatorResponse = _cartMediator.AddToCart(viewmodel.ProductId, 1);

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.AddToCart.Ok:
                    return Redirect("/cart");

                case MediatorCodes.CartMediator.AddToCart.Fail:
                default:
                    return Redirect("/cart");
            }
        }

        [HttpPost]
        public ActionResult Identify(CartIdentifyViewModel viewmodel)
        {
            var mediatorResponse = _cartMediator.IdenfifyContactInCart(viewmodel.Email, viewmodel.ReplaceExistingUserCart);

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.IdenfifyContactInCart.Ok:
                    return Redirect("/cart");

                case MediatorCodes.CartMediator.IdenfifyContactInCart.Fail:
                default:
                    return Redirect("/cart");
            }
        }

        [HttpPost]
        public ActionResult SetProperties(CartSetPropertiesViewModel viewmodel)
        {
            var mediatorResponse = _cartMediator.SetCartProperty("VatNumber", viewmodel.VatNumber);

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.SetCartProperty.Ok:
                    return Redirect("/cart");

                case MediatorCodes.CartMediator.SetCartProperty.Fail:
                default:
                    return Redirect("/cart");
            }
        }

        [HttpPost]
        public ActionResult CartDelete()
        {
            var mediatorResponse = _cartMediator.CartDelete();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.CartDelete.Ok:
                    return Redirect("/cart");

                case MediatorCodes.CartMediator.CartDelete.Fail:
                default:
                    return Redirect("/cart");
            }
        }

        [HttpPost]
        public ActionResult SessionAbandon()
        {
            var mediatorResponse = _cartMediator.SessionAbandon();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.SessionAbandon.Ok:
                    return Redirect("/cart");

                case MediatorCodes.CartMediator.SessionAbandon.Fail:
                default:
                    return Redirect("/cart");
            }
        }
    }
}