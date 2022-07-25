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
    public class MemberController : Controller
    {
        // Instantiation
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly UserService _userService;
        public MemberController()
        {
            _userService = new UserService();
        }
        public MemberController(UserService userService)
        {
            _userService = userService;
        }
        #endregion
        // Action Methods
        #region Action Methods
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(MemberVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                if (!_userService.ChechIfMemberExist(vmodel))
                {
                    hasSaved = _userService.CreateMember(vmodel);
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Form submitted successfully.";
                    return View(new MemberVM());
                }
                else
                {
                    ViewBag.ErrorMessage = "Member already exist.";
                    return View(vmodel);
                }
            }
            return View(vmodel);
        }
        #endregion
        // Methods
        #region Methods
    
        #endregion
    }
}