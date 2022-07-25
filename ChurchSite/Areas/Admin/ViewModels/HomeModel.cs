using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class HomeModel
    {
        public List<ArticleVM> FeaturedArticles { get; set; }
        public List<ArticleVM> SearchedArticles { get; set; }
        public List<CategoriesVM> CategoryNav { get; set; }
        public int SearchCount { get; set; }
    }
}