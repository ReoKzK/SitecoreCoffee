using System.Web.Mvc;
using Sitecore.Mvc.Controllers;
using SitecoreCoffee.Feature.Commerce.Services.CarPartsShop;
using SitecoreCoffee.Foundation.Infrastructure.Services;

namespace SitecoreCoffee.Feature.Commerce.Controllers
{
    public class CommercePageController : SitecoreController
    {
        private readonly IContextSwitchingService _contextSwitchingService;
        private readonly ICarPartsShopSystemService _carPartsShopSystemService;
        
        public CommercePageController()
        {
            _contextSwitchingService = DependencyResolver.Current.GetService<IContextSwitchingService>();
            _carPartsShopSystemService = DependencyResolver.Current.GetService<ICarPartsShopSystemService>();
        }

        public override ActionResult Index()
        {
            var heartbeat = _carPartsShopSystemService.Heartbeat();

            if (!heartbeat.IsAlive)
            {
                // - External commerce system is dead, so we are showing maintenance page -

                _contextSwitchingService.SwitchContextItem(Constants.Maintenance.ItemId);
            }

            return base.Index();
        }
    }
}