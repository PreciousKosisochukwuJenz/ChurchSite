using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // Instanciation
        #region Instanciation
        IApplicationSettingsService _settingsService;
        IUserService _userService;
        IArticleService _articleService;
        public DashboardController()
        {
            _articleService = new ArticleService(new DatabaseEntities());
            _userService = new UserService(new DatabaseEntities());
            _settingsService = new ApplicationSettingsService(new DatabaseEntities());
        }
        public DashboardController( ArticleService articleService, UserService userService, ApplicationSettingsService applicationSettingsService)
        {
            _userService = userService;
            _articleService = articleService;
            _settingsService = applicationSettingsService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion
        // GET: Admin/Dashboard
        public ActionResult Analytics()
        {
            ViewBag.Articles = db.Articles.Count(x => x.IsDeleted == false);
            ViewBag.Prayers = db.Prayers.Count(x => x.IsDeleted == false);
            ViewBag.Users = db.Users.Count(x => x.IsDeleted == false);
            ViewBag.ActiveUsers = db.Users.Count(x => x.IsDeleted == false && x.IsActive == true);
            ViewBag.InActiveUsers = db.Users.Count(x => x.IsDeleted == false && x.IsActive == false);
            ViewBag.Categories = db.Categories.Count(x => x.IsDeleted == false && x.ParentID == null);
            ViewBag.SubCategories = db.Categories.Count(x => x.IsDeleted == false && x.ParentID != null);


            // Chart
            List<int> repartition = new List<int>();
            int[] amList = { 500, 1000, 2000, 5000, 10000, 50000, 100000, 150000 };
            repartition.AddRange(amList);

            var amount = db.AfflilateBonusManagers.Where(x => x.IsDeleted == false).Select(x => new AfflilateBonusManager { Amount = x.Amount });

            ViewBag.Amount = amount;
            ViewBag.Rep = repartition;
            return View();
        }
    }
}