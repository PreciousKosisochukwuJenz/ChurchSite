﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Home()
        {
            return View();
        }
    }
}