using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // Instanciation
        #region Instanciation
        DatabaseEntities db = new DatabaseEntities();
        IUserService _userService;
        IFeeService _feeService;
        public HomeController()
        {
            _userService = new UserService(new DatabaseEntities());
            _feeService = new FeeService(new DatabaseEntities());
        }
        public HomeController(UserService userService,FeeService feeService)
        {
            _userService = userService;
            _feeService = feeService;
        }
        #endregion
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.RecentPost = db.Articles.Where(x => x.IsDeleted == false).Select(b => new ArticleVM()
            {
                Id = b.Id,
                Description = b.Description,
                // Image = b.Image,
                Title = b.Title,
                PublishedBy = b.PublishedBy,
                DateCreated = b.DateCreated,
                FilePath = b.FilePath
            }).OrderByDescending(x => x.Id).Take(2).ToList();

            ViewBag.Gallery = db.Galleries.Where(x => x.IsDeleted == false).OrderBy(x => x.Hierarchy).Take(6).ToList();
            ViewBag.Announcement = db.Announcements.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
            return View();
        }
        public ActionResult Donation()
        {
            return View();
        }
        public ActionResult Calender()
        {
            return View();
        }

        public ActionResult ProcessPayment(DonationVM donationVM)
       {
            var invoice = new InvoiceVM();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            var _baseUrl = "/FeeManagement/FeeManager/PayFees";
            invoice = _feeService.CreateInvoice(baseUrl + _baseUrl, donationVM);
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }

    }
}