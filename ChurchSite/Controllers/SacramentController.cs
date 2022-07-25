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
    public class SacramentController : Controller
    {
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly ContentService _contentService;
        public SacramentController()
        {
            _contentService = new ContentService();
        }
        public SacramentController(ContentService contentService)
        {
            _contentService = contentService;
        }
        #endregion

        // GET: Sacrament
        public ActionResult ViewSacrament(int id)
        {
            return View(_contentService.GetSacrament(id));
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}