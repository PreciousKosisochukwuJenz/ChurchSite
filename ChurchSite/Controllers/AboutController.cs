using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;

namespace ChurchSite.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        DatabaseEntities _db = new DatabaseEntities();
        IUserService _userService;
        public AboutController()
        {
            _userService = new UserService(new DatabaseEntities());
        }
        public AboutController(UserService userService)
        {
            _userService = userService;
        }
        public ActionResult AboutUs()
        {
            ViewBag.Spiritualists = _userService.GetSpiritualists();
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
      
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ContactUs(FormCollection vmodel)
        {
            if (ModelState.IsValid)
            {
                var model = new CustomMail()
                {
                    Name = vmodel["Name"],
                    Email = vmodel["Email"],
                    Subject = vmodel["Subject"],
                    Message = vmodel["Message"],
                    IsDeleted = false,
                    DateRecieved = DateTime.Now
                };
              
                _db.CustomMails.Add(model);
                _db.SaveChanges();
            }
            return View();
        }
        public ActionResult Staff()
        {
            ViewBag.Spiritualists = _userService.GetSpiritualists();
            return View();
        }
        public ActionResult MassSchedule()
        {
            return View();
        }
        public ActionResult ParishHistory()
        {
            return View();
        }
        public ActionResult AboutParish()
        {
            return View();
        }
        public ActionResult EnuguDiocese()
        {
            return View();
        }
        public ActionResult AboutPopeFrancis()
        {
            return View();
        }
        public ActionResult Direction()
        {
            return View();
        }
        public ActionResult Employment()
        {
            return View();
        }
        public ActionResult AboutBishop()
        {
            return View();
        }
        public ActionResult Employement()
        {
            return View();
        }
    }
}