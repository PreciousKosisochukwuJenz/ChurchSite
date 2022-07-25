using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class PrayerController : Controller
    {
        // GET: Admin/Prayer
        public ActionResult Index()
        {
            return View();
        }
    }
}