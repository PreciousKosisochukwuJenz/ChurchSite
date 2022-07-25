using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class GalleryVM
    {
        public int Id { get; set; }
        public int? CategoryID { get; set; }
        public string FilePath { get; set; }
        public byte[] ByteData { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public string Category { get; set; }
        public int? Hierarchy { get; set; }
    }
}