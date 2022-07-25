using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChurchSite.DAL.DataConnection;

namespace ChurchSite.Controllers
{
    public class GalleryController : Controller
    {
        #region Instanciation
        DatabaseEntities db = new DatabaseEntities();
#endregion
        // GET: Gallery
        public ActionResult Index()
        {
            ViewBag.Gallery = db.Galleries.Where(x => x.IsDeleted == false).OrderBy(x => x.Hierarchy).ToList();
            return View();
        }
    }
}