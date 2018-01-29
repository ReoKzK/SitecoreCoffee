using System.Web.Mvc;
using SitecoreCoffee.Feature.Commerce.Mediators;
using SitecoreCoffee.Feature.Commerce.Models;

namespace SitecoreCoffee.Feature.Commerce.Controllers
{
    public class CommerceAdminController : Controller
    {
        private readonly ICartMediator _cartMediator;

        public CommerceAdminController(ICartMediator cartMediator)
        {
            _cartMediator = cartMediator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(CartSearchModel search)
        {
            var mediatorResponse = _cartMediator.SearchCarts(search);

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.CartMediator.SearchCarts.Ok:
                    return PartialView(mediatorResponse.ViewModel);

                case MediatorCodes.CartMediator.SearchCarts.Fail:
                default:
                    return PartialView();
            }
        }
    }
}