using ChurchSite.Areas.Admin.Services;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Controllers
{

    [AllowAnonymous]
    public class PrayerController : Controller
    {
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly ContentService _contentService;
        public PrayerController()
        {
            _contentService = new ContentService();
        }
        public PrayerController(ContentService contentService)
        {
            _contentService = contentService;
        }
        #endregion

        // GET: Prayer
        public ActionResult ViewPrayer(int id)
        {
            return View(_contentService.GetPrayer(id));
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}