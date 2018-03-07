using System.Web.Mvc;
using Sitecore.Mvc.Controllers;
using SitecoreCoffee.Feature.Commerce.Services.CarPartsShop;
using SitecoreCoffee.Foundation.Configuration.Models;
using SitecoreCoffee.Foundation.Configuration.Services;
using SitecoreCoffee.Foundation.Infrastructure.Services;

namespace SitecoreCoffee.Feature.Commerce.Controllers
{
    public class CommercePageController : SitecoreController
    {
        private readonly IContextSwitchingService _contextSwitchingService;
        private readonly ICarPartsShopSystemService _carPartsShopSystemService;
        private readonly ISiteConfigurationService _siteConfigurationService;
        
        public CommercePageController()
        {
            _contextSwitchingService = DependencyResolver.Current.GetService<IContextSwitchingService>();
            _carPartsShopSystemService = DependencyResolver.Current.GetService<ICarPartsShopSystemService>();
            _siteConfigurationService = DependencyResolver.Current.GetService<ISiteConfigurationService>();
        }

        public override ActionResult Index()
        {
            var heartbeat = _carPartsShopSystemService.Heartbeat();
            var siteMaintenanceMode = _siteConfigurationService.GetConfiguration<IMaintenanceSettings>()?.MaintenanceModeOn ?? false;

            if ((!heartbeat.IsAlive || siteMaintenanceMode) && !_contextSwitchingService.IsExperienceEditor)
            {
                // - External commerce system is dead, so we are showing maintenance page -

                _contextSwitchingService.SwitchContextItem(Constants.Maintenance.ItemId);
            }

            return base.Index();
        }
    }
}