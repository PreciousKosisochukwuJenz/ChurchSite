using ChurchSite.Areas.Admin.Interfaces;
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
    public class SpiritualistController : Controller
    {
        // Instanciation
        #region Instanciation
        DatabaseEntities db = new DatabaseEntities();
        IUserService _userService;
        public SpiritualistController()
        {
            _userService = new UserService(new DatabaseEntities());
        }
        public SpiritualistController(UserService userService)
        {
            _userService = userService;
        }
        #endregion


        // GET: Admin/Spiritualist
        public ActionResult Manage(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Spiritualist added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Spiritualist updated successfully.";
            }
            ViewBag.Spiritualist = _userService.GetSpiritualists();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateSpiritualist(SpiritualistVM vmodel, HttpPostedFileBase image)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.AddSpiritualist(vmodel, image);
            }
            return RedirectToAction("Manage", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSpiritualist(SpiritualistVM vmodel,HttpPostedFileBase image)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.UpdateSpiritualist(vmodel,image);
            }
            return RedirectToAction("Manage", new { Editted = hasSaved });
        }
        public JsonResult GetSpiritualist(int id)
        {
            var model = _userService.GetSpiritualist(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteSpiritualist(int id)
        {
            var model = _userService.DeleteSpiritualist(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateHierarchy(int[] data)
        {
            var hasUpdated = _userService.UpdateHierarchy(data);
            return RedirectToAction("Manage", new { Updated = hasUpdated });
        }
    }
}