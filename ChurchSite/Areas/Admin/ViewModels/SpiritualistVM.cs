using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class SpiritualistVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public byte[] Photo { get; set; }
        public string FilePath { get; set; }
        public string ImageString { get; set; }
        public int? Hierarchy { get; set; }
    }
}