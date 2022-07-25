using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChurchSite.DAL.DataConnection;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {  // Instanciation
        #region Instanciation
        IApplicationSettingsService _settingService;
        IArticleService _articleService;
        public ArticleController()
        {
            _settingService = new ApplicationSettingsService(new DatabaseEntities());
            _articleService = new ArticleService(new DatabaseEntities());
        }
        public ArticleController(ApplicationSettingsService settingsService, ArticleService articleService)
        {
            _settingService = settingsService;
            _articleService = articleService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion

        // Article
        public ActionResult ManageArticles(bool? Added,bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Article added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Article updated successfully.";
            }
            ViewBag.Categories = new SelectList(db.Categories.Where(x => x.IsDeleted == false && x.ParentID == null), "Id", "Description", "ParentDescription", true);
            ViewBag.SubCategories = new SelectList(db.Categories.Where(x => x.IsDeleted == false && x.ParentID != null), "Id", "Description", "ParentDescription", true);
            ViewBag.Articles = _articleService.GetArticles();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateArticle(ArticleVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _articleService.CreateArticle(vmodel,ImageData);
            }
            return RedirectToAction("ManageArticles", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditArticle(ArticleVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _articleService.EditArticle(vmodel,ImageData);
            }
            return RedirectToAction("ManageArticles", new { Editted = hasSaved });
        }
        public JsonResult GetArticle(int id)
        {
            var model = _articleService.GetArticle(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedSubCategory(int id)
        {
            var model = _articleService.GetSelectedSubCategories(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteArticle(int id)
        {
            var model = _articleService.DeleteArticle(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Categories
        public ActionResult ManageArticleContents(bool? Saved, int ArticleID)
        {
            if (Saved == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Save successfully.";
            }
            ViewBag.Article = _articleService.GetArticle(ArticleID);
            ViewBag.ArticleContents = _articleService.GetArticleContents(ArticleID);
            return View();
        }
        public ActionResult CreateArticleContent(int ArticleID)
        {
            ViewBag.Article = _articleService.GetArticle(ArticleID);
            var model = new ArticleContentVM();
            model.ArticleID = ArticleID;
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateArticleContent(ArticleContentVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _articleService.CreateArticleContent(vmodel);
            }
            return RedirectToAction("ManageArticleContents", new { Saved = hasSaved, ArticleID = vmodel.ArticleID });
        }
        public ActionResult EditArticleContent(int Id, int ArticleID)
        {
            ViewBag.Article = _articleService.GetArticle(ArticleID);
            var model = _articleService.GetArticleContent(Id);
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditArticleContent(ArticleContentVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _articleService.EditArticleContent(vmodel);
            }
            return RedirectToAction("ManageArticleContents", new { Saved = hasSaved, ArticleID = vmodel.ArticleID });
        }
        public JsonResult DeleteArticleContent(int id)
        {
            var model = _articleService.DeleteArticleContent(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}