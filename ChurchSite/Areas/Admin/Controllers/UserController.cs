using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // Instanciation
        #region Instanciation
        DatabaseEntities db = new DatabaseEntities();
        IUserService _userService;
        public UserController()
        {
            _userService = new UserService(new DatabaseEntities());
        }
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        #endregion

        public ActionResult Manage(bool? Added,bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "User added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "User updated successfully.";
            }
            ViewBag.Roles = new SelectList(db.Roles.Where(x => x.IsDeleted == false), "Id", "Description");
            ViewBag.Users = _userService.GetUsers();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateUser(UserVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.CreateUser(vmodel);
            }
            return RedirectToAction("Manage", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditUser(UserVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.EditUser(vmodel);
            }
            return RedirectToAction("Manage", new { Editted = hasSaved });
        }
        public JsonResult GetUser(int id)
        {
            var model = _userService.GetUser(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUser(int id)
        {
            var model = _userService.DeleteUser(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeactivateUser(int id)
        {
            var model = _userService.DeactivateUser(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActivateUser(int id)
        {
            var model = _userService.ActivateUser(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Role
        public ActionResult ManageRoles(bool? Added,bool? Editted)
        {
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Role added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Role updated successfully.";
            }
            ViewBag.Roles = _userService.GetRoles();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateRole(RoleVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.CreateRole(vmodel);
            }
            return RedirectToAction("ManageRoles", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditRole(RoleVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _userService.EditRole(vmodel);
            }
            return RedirectToAction("ManageRoles", new { Editted = hasSaved });
        }
        public JsonResult GetRole(int id)
        {
            var model = _userService.GetRole(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRole(int id)
        {
            var model = _userService.DeleteRole(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManagePermissions(int RoleID,bool? Saved)
        {
            if (Saved == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertMessage"] = "Permissions updated successfully";
                TempData["AlertType"] = "alert-success";
            }
            return View(_userService.GetAssignedPermission(RoleID));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagePermissions(RolePermissionVM rolePermissionVM)
        {
            bool hasSaved =  false;
            var model = new RolePermissionVM();
            int roleID = 0;
            if (ModelState.IsValid)
            {
                var rolePermission = new RolePermission();
                foreach (var item in rolePermissionVM.TableDataSource)
                {
                    roleID = item.RoleID;
                    if (_userService.CheckPermissionExist(item.RoleID, item.PermissionID) == 0)
                    {
                        rolePermission = _userService.SavePermission(item);
                    }
                    else
                    {
                        _userService.AssignPermission(item);
                    }
                }
            }
            hasSaved= true;
            return ManagePermissions(roleID, hasSaved);
        }
        public ActionResult ManageAfflilates()
        {
            ViewBag.ArticleBounsSummary = new List<ArticleVM>();
            var model = _userService.GetAfflilate_Bouns();
            return View(model);
        }
        [HttpPost]
        public ActionResult ArticleBonusSummary(int afflilateID)
        {
            System.Threading.Thread.Sleep(1500);
            ViewBag.ArticleBounsSummary = _userService.GetArticleAddedByAfflilate(afflilateID);
            return PartialView("_ArticleBonusSummary");
        }
    }
}