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
using ChurchSite.Areas.Admin.Interfaces;

namespace ChurchSite.Controllers
{
    public class ApplicationController : Controller
    {
        // Instantiation
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        private readonly IApplicationService _applicationService;
        private readonly IFeeService _feeService;

        public ApplicationController()
        {
            _applicationService = new ApplicationService(new DatabaseEntities());
            _feeService = new FeeService(new DatabaseEntities());
        }
        public ApplicationController(ApplicationService applicationService, FeeService feeService)
        {
            _applicationService = applicationService;
            _feeService = feeService;
        }
        #endregion
        // Action Methods
        #region Action Methods
        [AllowAnonymous]
        public ActionResult Baptism()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Baptism(BaptismVM vmodel)
        {
            if (ModelState.IsValid)
            {
                _applicationService.CreateBaptism(vmodel);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Request submitted successfully.";
                return View(new BaptismVM());
            }
            return View(vmodel);
        }
        [AllowAnonymous]
        public ActionResult Eucharist()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Eucharist(EucharistVM vmodel)
        {
            if (ModelState.IsValid)
            {
                _applicationService.CreateEucharist(vmodel);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Request submitted successfully.";
                return View(new EucharistVM());
            }
            return View(vmodel);
        }
        [AllowAnonymous]
        public ActionResult Matrimony()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Matrimony(MatrimonyVM vmodel)
        {
            if (ModelState.IsValid)
            {
                _applicationService.CreateMatrimony(vmodel);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Request submitted successfully.";
                return View(new MatrimonyVM());
            }
            return View(vmodel);
        }
        [AllowAnonymous]
        public ActionResult BookMass()
        {
            var intension = new List<SelectListItem>();
            intension.AddRange(Enum.GetValues(typeof(Intensions)).Cast<Intensions>().Select(
                        (item, index) => new SelectListItem
                        {
                            Text = item.ToString(),
                            Value = ((int)item).ToString(),
                        }).ToList());
            var mass = new List<SelectListItem>();
            mass.AddRange(Enum.GetValues(typeof(Mass)).Cast<Mass>().Select(
                        (item, index) => new SelectListItem
                        {
                            Text = item.ToString(),
                            Value = ((int)item).ToString(),
                        }).ToList());

            ViewBag.Intensions = intension;
            ViewBag.Mass = mass;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult BookMass(BookMassVM vmodel)
        {
            var intension = new List<SelectListItem>();
            intension.AddRange(Enum.GetValues(typeof(Intensions)).Cast<Intensions>().Select(
                        (item, index) => new SelectListItem
                        {
                            Text = item.ToString(),
                            Value = ((int)item).ToString(),
                        }).ToList());
            var mass = new List<SelectListItem>();
            mass.AddRange(Enum.GetValues(typeof(Mass)).Cast<Mass>().Select(
                        (item, index) => new SelectListItem
                        {
                            Text = item.ToString(),
                            Value = ((int)item).ToString(),
                        }).ToList());

            ViewBag.Intensions = intension;
            ViewBag.Mass = mass;
            if (ModelState.IsValid)
            {
                _applicationService.CreateBookMass(vmodel);
                ModelState.Clear();
                ViewBag.SuccessMessage = "Request submitted successfully.";
                return View("_BookMassInvoiceGeneration", vmodel);
            }
            return View(vmodel);
        }
        public ActionResult ProcessPayment(BookMassVM vmodel)
        {
            var invoice = new InvoiceVM();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            switch (vmodel.command)
            {
                case "mass booking":
                    var _baseUrl = "/FeeManagement/FeeManager/PayFees";
                    invoice = _feeService.CreateBookMassInvoice(baseUrl + _baseUrl, vmodel);
                    return Json(invoice, JsonRequestBehavior.AllowGet);
            }
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}