using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Controllers
{
    public class AccountController : Controller
    {
        // Instantiation
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly UserService _userService;
        public AccountController()
        {
            _userService = new UserService();
        }
        public AccountController(UserService userService)
        {
            _userService = userService;
        }
        #endregion
        // Action Methods
        #region Action Methods
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM userVM)
        {
            Session.Clear();
            var user = _userService.CheckCreditials(userVM);
            if (user.Count > 0)
            {
                LoginSession(user);
                if (Session["UserId"] != null)
                {
                    Global.AuthenticatedUserID = Convert.ToInt32(Session["UserId"].ToString());
                    var UserID = Convert.ToInt32(Session["UserId"]);
                    var RoleID = db.Users.Where(x => x.Id == UserID).FirstOrDefault().RoleID;
                    var PermissionList = db.RolePermissions.Where(x => x.RoleID == RoleID).ToList();
                    // User management
                    Session["ManageUsers"] = PermissionList.Where(p => p.Permission.Description == "Manage users").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage users").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageSpiritualist"] = PermissionList.Where(p => p.Permission.Description == "Manage Spiritualist").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage Spiritualist").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageRoles"] = PermissionList.Where(p => p.Permission.Description == "Manage roles").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage roles").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageAfflilates"] = PermissionList.Where(p => p.Permission.Description == "Manage Afflilates").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage Afflilates").FirstOrDefault().IsAssigned.ToString();

                    if (Session["ManageUsers"].ToString() == "True" || Session["ManageRoles"].ToString() == "True" || Session["ManageAfflilates"].ToString() == "True" || Session["ManageSpiritualist"].ToString() == "True")
                    {
                        Session["UserManagement"] = "True";
                    }
                    else
                    {
                        Session["UserManagement"] = "False";
                    }
                    // Settings management
                    Session["ManageFees"] = PermissionList.Where(p => p.Permission.Description == "Manage Fee").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage Fee").FirstOrDefault().IsAssigned.ToString();
                    Session["ApplicationSettings"] = PermissionList.Where(p => p.Permission.Description == "Application settings").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Application settings").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageCategories"] = PermissionList.Where(p => p.Permission.Description == "Manage categories").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage categories").FirstOrDefault().IsAssigned.ToString();
                    if (Session["ApplicationSettings"].ToString() == "True" || Session["ManageCategories"].ToString() == "True" || Session["ManageFees"].ToString() == "True")
                    {
                        Session["SettingsManagement"] = "True";
                    }
                    else
                    {
                        Session["SettingsManagement"] = "False";
                    }
                    // Article
                    Session["ManageArticles"] = PermissionList.Where(p => p.Permission.Description == "Manage articles").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage articles").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageAnnouncement"] = PermissionList.Where(p => p.Permission.Description == "Manage Announcement").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage Announcement").FirstOrDefault().IsAssigned.ToString();
                    Session["ManagePrayers"] = PermissionList.Where(p => p.Permission.Description == "Manage prayers").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage prayers").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageOrganisations"] = PermissionList.Where(p => p.Permission.Description == "Manage organisations").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage organisations").FirstOrDefault().IsAssigned.ToString();
                     Session["ManageSacraments"] = PermissionList.Where(p => p.Permission.Description == "Manage sacrament").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage sacrament").FirstOrDefault().IsAssigned.ToString();
                    Session["ManageGalleries"] = PermissionList.Where(p => p.Permission.Description == "Manage galleries").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Manage galleries").FirstOrDefault().IsAssigned.ToString();
                    if (Session["ManageArticles"].ToString() == "True" || Session["ManageAnnouncement"].ToString() == "True" || Session["ManagePrayers"].ToString() == "True" || Session["ManageOrganisations"].ToString() == "True" || Session["ManageSacraments"].ToString() == "True" || Session["ManageGalleries"].ToString() == "True")
                    {
                        Session["ContentManagement"] = "True";
                    }
                    else
                    {
                        Session["ContentManagement"] = "False";
                    }
                    // Dashboard
                    Session["Analytics"] = PermissionList.Where(p => p.Permission.Description == "Analytics").Count() == 0 ? "false" : PermissionList.Where(x => x.Permission.Description == "Analytics").FirstOrDefault().IsAssigned.ToString();
                    if (Session["Analytics"].ToString() == "True")
                    {
                        Session["DashboardManagement"] = "True";
                    }
                    else
                    {
                        Session["DashboardManagement"] = "False";
                    }
                }
                return RedirectToAction("Home", "Home", new { area = "Admin" });
            }
            else
            {
                ViewBag.Error = true;
            }
            return View(userVM);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            if (Session["UserId"] != null)
            {
                Session.Clear();
            }
            return RedirectToAction("Login", "Account", new { area = "" });
        }
        [AllowAnonymous]
        public JsonResult CheckSessionExists()
        {
            bool IsExisting = false;
            if (Session["UserId"] == null)
                IsExisting = false;
            else
                IsExisting = true;

            return Json(IsExisting, JsonRequestBehavior.AllowGet);
        }
        #endregion
        // Methods
        #region Methods
        public void LoginSession(List<User> user)
        {
            var Firstname = user.FirstOrDefault().Firstname;
            var Lastname = user.FirstOrDefault().Lastname;
            var DateCreated = user.FirstOrDefault().DateCreated;
            var Email = user.FirstOrDefault().Email;
            var Username = user.FirstOrDefault().Username;
            var ID = user.FirstOrDefault().Id;
            var Role = user.FirstOrDefault().Role.Description;

            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, Firstname),
                    new Claim(ClaimTypes.Name, Lastname),
                    new Claim(ClaimTypes.DateOfBirth, DateCreated.ToString()),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.PrimarySid, ID.ToString()),
                    new Claim(ClaimTypes.Name, Username)
                },
                "ApplicationCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignIn(identity);
            Session["UserId"] = ID.ToString();
            Session["Username"] = Username;
            Session["DateCreated"] = Convert.ToDateTime(DateCreated).ToLongDateString();
            Session["Name"] = Firstname + " " + Lastname;
            Session["Role"] = Role;
        }
        #endregion
    }
}