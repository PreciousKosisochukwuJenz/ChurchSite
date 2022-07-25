using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.Services;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    public class MailController : Controller
    {
        #region Instanciation
        IMailService _mailService;
        public MailController()
        {
            _mailService = new MailService(new DatabaseEntities());
        }
        public MailController( MailService mailService)
        {
            _mailService = mailService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion

        // GET: Admin/Mail
        public ActionResult Mails()
        {
            ViewBag.UnreadMails = _mailService.GetNotReadMails();
            ViewBag.AllMails = _mailService.GetAllMails();
            return View();
        }

        [HttpPost]
        public ActionResult Mails(int[] data,string command)
        {
            if(data != null)
            {
                switch (command)
                {
                    case "delete":
                        foreach (var id in data)
                        {
                            _mailService.DeleteMail(id);
                        }

                        ViewBag.ShowAlert = true;
                        TempData["AlertType"] = "alert-success";
                        TempData["AlertMessage"] = "Mail(s) deleted successfully.";
                        break;
                    case "markasread":
                        foreach (var id in data)
                        {
                            _mailService.MarkMailAsRead(id);
                        }
                        break;
                }
            }
            ViewBag.UnreadMails = _mailService.GetNotReadMails();
            ViewBag.AllMails = _mailService.GetAllMails();
            return View();
        }
    }
}