using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public byte[] Image { get; set; }
        public string ImageString { get; set; }
        public int? CategoryID { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsFeatured { get; set; }
        public string Title { get; set; }
        public string PublishedBy { get; set; }
        public int[] SubCategories { get; set; }
        public List<ArticleContentVM> ArticleContent { get; set; }
    }
}