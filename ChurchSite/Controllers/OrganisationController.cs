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
    public class OrganisationController : Controller
    {
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly ContentService _contentService;
        public OrganisationController()
        {
            _contentService = new ContentService();
        }
        public OrganisationController(ContentService contentService)
        {
            _contentService = contentService;
        }
        #endregion

        // GET: Organisation
        public ActionResult ViewOrganisation(int id)
        {
            return View(_contentService.GetOrganisation(id));
        }
    }
}