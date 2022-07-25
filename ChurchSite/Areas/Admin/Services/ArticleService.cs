using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using eLibrarySystem.Areas.Admin.Helpers;
using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchSite.Areas.Admin.Services
{
    public class ArticleService : IArticleService
    {

        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        public ArticleService()
        {
            _db = new DatabaseEntities();
        }
        public ArticleService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion

        // Article
        public List<ArticleVM> GetArticles()
        {
            var model = _db.Articles.Where(x => x.IsDeleted == false).Select(b => new ArticleVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
              //  Image = b.Image,
                FilePath = b.FilePath,
                PublishedBy =b.PublishedBy,
                CategoryID = b.CategoryID,
                Category = b.Category.Description,
                IsFeatured = b.IsFeatured,
            }).OrderByDescending(x => x.Id).ToList();
            return model;
        }
        public bool CreateArticle(ArticleVM Vmodel, HttpPostedFileBase ImageData)
        {
            string filename = ImageData.FileName;
            string path = "/Front_Content/img/CKP/ARTICLE/";
            string targetpath = HttpContext.Current.Server.MapPath("~" + path);
            ImageData.SaveAs(targetpath + filename);

            bool hasSaved = false;
            Article model = new Article()
            {
                Description = Vmodel.Description,
                Title = Vmodel.Title,
                PublishedBy = Vmodel.PublishedBy,
                //Image = CustomSerializer.Serialize(ImageData),
                FilePath = path + filename,
                CategoryID = Vmodel.CategoryID,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                CreatedByID = Global.AuthenticatedUserID,
                IsFeatured = Vmodel.IsFeatured
            };
            _db.Articles.Add(model);

            // Save Book Sub Categories
            if (Vmodel.SubCategories.Count() > 0)
                AddArticleSubCategories(model.Id, Vmodel.SubCategories);

            _db.SaveChanges();

            // Add bonus
            var AfflilateBonus = _db.ApplicationSettings.FirstOrDefault().AfflilateArticleBonus;
            var AfflilateBonusRowExists = _db.AfflilateBonusManagers.Count(x => x.AfflilateUserID == Global.AuthenticatedUserID && x.BonusType == BonusType.Article);
            if (AfflilateBonusRowExists > 0)
            {
                var afflilateBonusRow = _db.AfflilateBonusManagers.FirstOrDefault(x => x.AfflilateUserID == Global.AuthenticatedUserID && x.BonusType == BonusType.Article);
                afflilateBonusRow.Amount += AfflilateBonus;
                afflilateBonusRow.LastModified = DateTime.Now;
                _db.Entry(afflilateBonusRow).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                var afflilateBonusRow = new AfflilateBonusManager()
                {
                    AfflilateUserID = Global.AuthenticatedUserID,
                    BonusType = BonusType.Article,
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    Amount = AfflilateBonus
                };
                _db.AfflilateBonusManagers.Add(afflilateBonusRow);
                _db.SaveChanges();
            }
            hasSaved = true;
            return hasSaved;
        }
        public ArticleVM GetArticle(int ID)
        {
            var model = _db.Articles.Where(x => x.Id == ID).Select(b => new ArticleVM()
            {
                Id = b.Id,
                Title = b.Title,
                PublishedBy = b.PublishedBy,
                Description = b.Description,
               // Image = b.Image,
               FilePath = b.FilePath,
                CategoryID = b.CategoryID,
                IsFeatured = b.IsFeatured
            }).FirstOrDefault();
            //model.ImageString = Convert.ToBase64String(model.Image);
            return model;
        }
        public bool EditArticle(ArticleVM Vmodel, HttpPostedFileBase ImageData)
        {
            bool hasSaved = false;
            Article model = _db.Articles.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Description = Vmodel.Description;
            model.Title = Vmodel.Title;
            model.PublishedBy = Vmodel.PublishedBy;
            model.Image = null;
            //if(ImageData != null)
            //    model.Image = CustomSerializer.Serialize(ImageData);

            if(ImageData != null)
            {
                string filename = ImageData.FileName;
                string path = "/Front_Content/img/CKP/ARTICLE/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                ImageData.SaveAs(targetpath + filename);
            }
            model.CategoryID = Vmodel.CategoryID;
            model.IsFeatured = Vmodel.IsFeatured;
            model.EdittedUserID = Global.AuthenticatedUserID;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            RemoveAllSubCategories(Vmodel.Id, Vmodel.SubCategories);
            if (Vmodel.SubCategories != null)
                AddArticleSubCategories(model.Id, Vmodel.SubCategories);

            hasSaved = true;
            return hasSaved;
        }
        public bool DeleteArticle(int ID)
        {
            bool hasSaved = false;
            var model = _db.Articles.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Article Content
        public List<ArticleContentVM> GetArticleContents(int ArticleID)
        {
            var model = _db.ArticleContents.Where(x => x.IsDeleted == false && x.ArticleID == ArticleID).Select(b => new ArticleContentVM()
            {
                Id = b.Id,
                Heading = b.Heading,
                Body = b.Body,
                ArticleID = b.ArticleID
            }).ToList();
            return model;
        }
        public bool CreateArticleContent(ArticleContentVM Vmodel)
        {
            bool hasSaved = false;
            ArticleContent model = new ArticleContent()
            {
                Heading = Vmodel.Heading,
                Body = Vmodel.Body,
                ArticleID = Vmodel.ArticleID,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                CreatedUserID = Global.AuthenticatedUserID,
            };
            _db.ArticleContents.Add(model);
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public ArticleContentVM GetArticleContent(int ID)
        {
            var model = _db.ArticleContents.Where(x => x.Id == ID).Select(b => new ArticleContentVM()
            {
                Id = b.Id,
                Heading = b.Heading,
                Body = b.Body,
                ArticleID = b.ArticleID
            }).FirstOrDefault();
            return model;
        }
        public bool EditArticleContent(ArticleContentVM Vmodel)
        {
            bool hasSaved = false;
            ArticleContent model = _db.ArticleContents.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Heading = Vmodel.Heading;
            model.Body = Vmodel.Body;
            model.ArticleID = Vmodel.ArticleID;
            model.EdittedUserID = Global.AuthenticatedUserID;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public bool DeleteArticleContent(int ID)
        {
            bool hasSaved = false;
            var model = _db.ArticleContents.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public List<ArticleVM> GetSearch(string query)
        {
            byte[] empty = { 4, 3 };
            var articles = _db.Articles.Where(x => x.Description.StartsWith(query) && x.IsDeleted == false)
                .Select(b => new ArticleVM()
                {
                    Id = b.Id,
                    Description = b.Description,
                   // Image = b.Image == null ? empty : b.Image,
                   FilePath =  b.FilePath,
                    Category = b.Category.Description
                }).ToList();

            return articles;
        }

        public void AddArticleSubCategories(int articleID, int[] SubCategoriesID)
        {
            foreach (var subcategoryID in SubCategoriesID)
            {
                if (_db.SubCategoryXArticles.Count(x => x.ArticleID == articleID && x.SubcategoryID == subcategoryID && x.IsDeleted == false) == 0)
                {
                    var model = new SubCategoryXArticle()
                    {
                        ArticleID = articleID,
                        SubcategoryID = subcategoryID,
                        IsDeleted = false
                    };
                    _db.SubCategoryXArticles.Add(model);
                }
            }
            _db.SaveChanges();
        }
        public void RemoveAllSubCategories(int articleID, int[] selectedSubCategories)
        {
            var articlesubcategories = _db.SubCategoryXArticles.Where(x => x.ArticleID == articleID && x.IsDeleted == false).ToList();
            if (selectedSubCategories == null)
            {
                foreach (var subcategory in articlesubcategories)
                {
                    subcategory.IsDeleted = true;
                    _db.Entry(subcategory).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            else
            {
                foreach (var subcategory in articlesubcategories)
                {
                    if (!selectedSubCategories.Contains(subcategory.Id))
                    {
                        subcategory.IsDeleted = true;
                        _db.Entry(subcategory).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
            }

        }
        public List<SelectListItem> GetSelectedSubCategories(int articleID)
        {
            var articleSubCategories = _db.SubCategoryXArticles.Where(x => x.IsDeleted == false && x.ArticleID == articleID).Select(b => b.SubcategoryID).ToList();
            List<SelectListItem> items = _db.Categories.Where(x => x.IsDeleted == false && x.ParentID != null).Select(b => new SelectListItem()
            {
                Text = b.Description,
                Value = b.Id.ToString(),
                Selected = articleSubCategories.Contains(b.Id),
            }).ToList();
            return items;
        }

    }
}