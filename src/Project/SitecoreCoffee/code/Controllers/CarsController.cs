using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreCoffee.Project.SitecoreCoffee.Controllers
{
    public class CarsController : Controller
    {
        public ActionResult Listing()
        {
            return View();
        }
    }
}