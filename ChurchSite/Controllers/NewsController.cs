using ChurchSite.Areas.Admin.Services;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        #region Instantiation
        private DatabaseEntities db = new DatabaseEntities();
        public readonly ContentService _contentService;
        private readonly ArticleService _articleService;
        public NewsController()
        {
            _contentService = new ContentService();
            _articleService = new ArticleService();
        }
        public NewsController(ContentService contentService, ArticleService articleService)
        {
            _contentService = contentService;
            _articleService = articleService;
        }
        #endregion

        // GET: News
        public ActionResult ViewNews(int id)
        {
            var model = db.Articles.Where(x => x.Id == id).Select(b => new ArticleVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
               // Image = b.Image
               FilePath =b.FilePath,
            }).FirstOrDefault();
            model.ArticleContent = db.ArticleContents.Where(x => x.ArticleID == id).Select(b=> new ArticleContentVM() { Id = b.Id,Heading = b.Heading, Body = b.Body}).ToList();
            return View(model);
            
        }

        public ActionResult NewsArticles()
        {
            var articles = _articleService.GetArticles();
            return View(articles);
        }

        public ActionResult Search(string q)
        {
            var model = db.Articles.Where(x => x.Title.StartsWith(q)).Select(b=> new ArticleVM() {

                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
               // Image = b.Image
               FilePath = b.FilePath,
            }).OrderByDescending(x => x.Id).ToList();
            return View(model);
        }

        public ActionResult Category(int id)
        {
            var model = db.Articles.Where(x => x.IsDeleted == false && x.CategoryID == id).Select(b => new ArticleVM()
            {

                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
              //  Image = b.Image
              FilePath = b.FilePath,
            }).OrderByDescending(x=>x.Id).ToList();
            return View(model);
        }

        public ActionResult TaggedNews(int id)
        {
            //var model = db.SubCategoryXArticles.Where(x => x.SubcategoryID == id).Select(b => new ArticleVM()
            //{
            //    Id = b.,
            //    Image = b.article.Image,
            //    Description = b.article.Description,
            //    Title = b.article.Title
            //}).OrderByDescending(x => x.Id).ToList();
            //return View(model);
            return View();
        }
    }
}