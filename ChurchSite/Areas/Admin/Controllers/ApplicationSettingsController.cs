﻿using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class ApplicationSettingsController : Controller
    {
        // Instanciation
        #region Instanciation
        IApplicationSettingsService _settingService;
        public ApplicationSettingsController()
        {
            _settingService = new ApplicationSettingsService(new DatabaseEntities());
        }
        public ApplicationSettingsController(ApplicationSettingsService settingsService)
        {
            _settingService = settingsService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion

        public ActionResult Manage()
        {
            return View(_settingService.GetApplicationSettings());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ApplicationSettingsVM Vmodel, HttpPostedFileBase LogoData, HttpPostedFileBase FaviconData)
        {
            if (ModelState.IsValid)
            {
                bool saveState = _settingService.UpdateApplicationSettings(Vmodel, LogoData, FaviconData);
                if(saveState == true)
                {
                    ViewBag.ShowAlert = true;
                    TempData["AlertMessage"] = "Application settings updated successfully.";
                    TempData["AlertType"] = "alert-success";
                }
            }
            return View(_settingService.GetApplicationSettings());
        }

        // Categories
        public ActionResult ManageCategories(bool? Added, bool? Editted, bool? AddedSub, bool? EdittedSub)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Category added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Category updated successfully.";
            }
            if (AddedSub == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sub-category added successfully.";
            }
            if (EdittedSub == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sub-category updated successfully.";
            }
            ViewBag.CategoryDD = new SelectList(db.Categories.Where(x => x.IsDeleted == false && x.ParentID == null), "Id", "Description");
            ViewBag.Categories = _settingService.GetCategories();
            ViewBag.SubCategories = _settingService.GetSubCategories();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCategory(CategoriesVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _settingService.CreateCategory(vmodel);
            }
            return RedirectToAction("ManageCategories", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditCategory(CategoriesVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _settingService.EditCategory(vmodel);
            }
            return RedirectToAction("ManageCategories", new { Editted = hasSaved });
        }
        public JsonResult GetCategory(int id)
        {
            var model = _settingService.GetCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCategory(int id)
        {
            var model = _settingService.DeleteCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Sub Categories
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateSubCategory(CategoriesVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _settingService.CreateSubCategory(vmodel);
            }
            return RedirectToAction("ManageCategories", new { AddedSub = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSubCategory(CategoriesVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _settingService.EditSubCategory(vmodel);
            }
            return RedirectToAction("ManageCategories", new { EdiitedSub = hasSaved });
        }
        public JsonResult GetSubCategory(int id)
        {
            var model = _settingService.GetSubCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteSubCategory(int id)
        {
            var model = _settingService.DeleteSubCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}