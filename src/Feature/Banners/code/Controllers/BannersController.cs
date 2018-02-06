using System.Web.Mvc;

namespace SitecoreCoffee.Feature.Banners.Controllers
{
    public class BannersController : Controller
    {
        public ActionResult Static()
        {
            return View();
        }
    }
}