using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChurchSite.DAL.DataConnection;
using System.IO;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {  // Instanciation
        #region Instanciation
        IApplicationSettingsService _settingService;
        IArticleService _articleService;
        IContentService _contentService;
        public ContentController()
        {
            _settingService = new ApplicationSettingsService(new DatabaseEntities());
            _articleService = new ArticleService(new DatabaseEntities());
            _contentService = new ContentService(new DatabaseEntities());
        }
        public ContentController(ContentService contentService,ApplicationSettingsService settingsService, ArticleService articleService)
        {
            _settingService = settingsService;
            _articleService = articleService;
            _contentService = contentService;
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

        // Prayer
        public ActionResult ManagePrayers(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Prayer added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Prayer updated successfully.";
            }
            ViewBag.Prayers = _contentService.GetPrayers();
            return View();
        }
        public ActionResult PrayerDetail(int id)
        {
            return View(_contentService.GetPrayer(id));
        }
        public ActionResult AddPrayer()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPrayer(PrayerVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.AddPrayer(vmodel, ImageData);
            }
            return RedirectToAction("ManagePrayers", new { Added = hasSaved });
        }
        public ActionResult EditPrayer(int Id)
        {
            return View(_contentService.GetPrayer(Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditPrayer(PrayerVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.UpdatePrayer(vmodel, ImageData);
            }
            return RedirectToAction("ManagePrayers", new { Editted = hasSaved });
        }
        public JsonResult DeletePrayer(int id)
        {
            _contentService.DeletePrayer(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        // Announcement
        public ActionResult ManageAnnouncement(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Announcement added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Announcement updated successfully.";
            }
            ViewBag.Announcement = _contentService.GetAnnouncement();
            return View();
        }
        public ActionResult AnnouncementDetail(int id)
        {
            return View(_contentService.GetAnnouncement(id));
        }
        public ActionResult AddAnnouncement()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.AddAnnouncement(vmodel, ImageData);
            }
            return RedirectToAction("ManageAnnouncement", new { Added = hasSaved });
        }
        public ActionResult EditAnnouncement(int Id)
        {
            return View(_contentService.GetAnnouncement(Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.UpdateAnnouncement(vmodel, ImageData);
            }
            return RedirectToAction("ManageAnnouncement", new { Editted = hasSaved });
        }
        public JsonResult DeleteAnnouncement(int id)
        {
            _contentService.DeleteAnnouncement(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        // Sacrament
        public ActionResult ManageSacraments(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sacrament added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sacrament updated successfully.";
            }
            ViewBag.Sacraments = _contentService.GetSacraments();
            return View();
        }
        public ActionResult SacramentDetail(int id)
        {
            return View(_contentService.GetSacrament(id));
        }
        public ActionResult AddSacrament()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddSacrament(SacramentVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.AddSacrament(vmodel, ImageData);
            }
            return RedirectToAction("ManageSacraments", new { Added = hasSaved });
        }
        public ActionResult EditSacrament(int Id)
        {
            return View(_contentService.GetSacrament(Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSacrament(SacramentVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.UpdateSacrament(vmodel, ImageData);
            }
            return RedirectToAction("ManageSacraments", new { Editted = hasSaved });
        }
        public JsonResult DeleteSacrament(int id)
        {
            _contentService.DeleteSacrament(id);
            return Json(JsonRequestBehavior.AllowGet);
        }


        // Organisation
        public ActionResult ManageOrganisations(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Organisation added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Organisation updated successfully.";
            }
            ViewBag.Organisations = _contentService.GetOrganisations();
            return View();
        }
        public ActionResult AddOrganisation()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrganisation(OrganisationVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.AddOrganisation(vmodel, ImageData);
            }
            return RedirectToAction("ManageOrganisations", new { Added = hasSaved });
        }
        public ActionResult OrganisationDetail(int id)
        {
            return View(_contentService.GetOrganisation(id));
        }
        public ActionResult EditOrganisation(int Id)
        {
            return View(_contentService.GetOrganisation(Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditOrganisation(OrganisationVM vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _contentService.UpdateOrganisation(vmodel, ImageData);
            }
            return RedirectToAction("ManageOrganisations", new { Editted = hasSaved });
        }
        public JsonResult DeleteOrganisation(int id)
        {
            _contentService.DeleteOrganisation(id);
            return Json(JsonRequestBehavior.AllowGet);
        }

        // Gallery
        public ActionResult ManageGalleries(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Gallery content added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Gallery content updated successfully.";
            }
            ViewBag.Galleries = _contentService.GetGalleries();
            return View();
        }
        public ActionResult AddGallery()
        {
            ViewBag.category = new SelectList(db.Categories.Where(x => x.IsDeleted == false && x.ParentID == null), "Id", "Description");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddGallery(GalleryVM vmodel, HttpPostedFileBase data)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                var start = data.FileName.IndexOf(".");
                var end = data.FileName.Length;
                var format = data.FileName.Substring(start + 1);
                hasSaved = _contentService.AddGallery(vmodel, data, format);
            }
            return RedirectToAction("ManageGalleries", new { Added = hasSaved });
        }

        public ActionResult GalleryDetail(int id)
        {
            return View(_contentService.GetGallery(id));
        }
        public ActionResult EditGallery(int Id)
        {
            ViewBag.category = new SelectList(db.Categories.Where(x => x.IsDeleted == false && x.ParentID == null), "Id", "Description");
            return View(_contentService.GetGallery(Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditGallery(GalleryVM vmodel, HttpPostedFileBase data)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                var start = data.FileName.IndexOf(".");
                var end = data.FileName.Length;
                var format = data.FileName.Substring(start + 1);
                hasSaved = _contentService.UpdateGallery(vmodel, data,format);
            }
            return RedirectToAction("ManageGalleries", new { Editted = hasSaved });
        }
        public JsonResult DeleteGallery(int id)
        {
            _contentService.DeleteGallery(id);
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult DownloadVideo(int id)
        {
            var model = db.Galleries.Where(x => x.Id == id).FirstOrDefault();
            return File(model.ByteData,model.ContentType,model.FileName);
        }
        public ActionResult UpdateHierarchy(int[] data)
        {
            var hasUpdated = _contentService.UpdateHierarchy(data);
            return RedirectToAction("ManageGalleries", new { Updated = hasUpdated });
        }
    }
}