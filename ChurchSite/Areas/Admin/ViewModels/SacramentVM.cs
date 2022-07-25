using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class SacramentVM
    {
        public int Id { get; set; }
        public string Title { get; set;  }
        public string Description { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string FilePath { get; set; }
        public byte[] Image { get; set; }
    }
}