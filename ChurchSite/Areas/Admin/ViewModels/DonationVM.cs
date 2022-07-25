using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class DonationVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
    }
}