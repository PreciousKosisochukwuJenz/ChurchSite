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
    public class FeeController : Controller
    {

        #region Instanciation
        IApplicationSettingsService _settingService;
        IFeeService _feeService;
        public FeeController()
        {
            _settingService = new ApplicationSettingsService(new DatabaseEntities());
            _feeService = new FeeService(new DatabaseEntities());
        }
        public FeeController(ApplicationSettingsService settingsService, FeeService feeService)
        {
            _settingService = settingsService;
            _feeService = feeService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion

        // GET: Admin/Fee
        public ActionResult Manage(bool? Added, bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Fee added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Fee updated successfully.";
            }
            ViewBag.Fees = _feeService.GetFees();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateFee(FeeVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _feeService.AddFee(vmodel);
            }
            return RedirectToAction("Manage", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditFee(FeeVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _feeService.UpdateFee(vmodel);
            }
            return RedirectToAction("Manage", new { Editted = hasSaved });
        }
        public JsonResult GetFee(int id)
        {
            var model = _feeService.GetFee(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFee(int id)
        {
            var model = _feeService.DeleteFee(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}