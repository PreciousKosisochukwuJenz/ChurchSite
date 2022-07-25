using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using eLibrarySystem.Areas.Admin.Helpers;
using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.Services
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        public ApplicationSettingsService()
        {
            _db = new DatabaseEntities();
        }
        public ApplicationSettingsService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion

        public ApplicationSettingsVM GetApplicationSettings()
        {
            byte[] emptyArr = { 4, 3 };
            var model = _db.ApplicationSettings.FirstOrDefault();
            var Vmodel = new ApplicationSettingsVM()
            {
                ID = model.Id,
                AppName = model.AppName,
                Logo = model.Logo == null ? emptyArr : model.Logo,
                Favicon = model.Favicon == null ? emptyArr : model.Favicon,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                LinkedInHandle = model.LinkedInHandle,
                FacebookHandle = model.FacebookHandle,
                InstagramHandle = model.InstagramHandle,
                TwitterHandle = model.TwitterHandle,
                AfflilateArticleBonusString = model.AfflilateArticleBonus.ToString(),
                AfflilateAuthorBonusString = model.AfflilateAuthorBonus.ToString(),
                AfflilateBookBonusString = model.AfflilateBookBonus.ToString()
            };
            return Vmodel;
        }
        public bool UpdateApplicationSettings(ApplicationSettingsVM Vmodel, HttpPostedFileBase Logo, HttpPostedFileBase Favicon)
        {
            bool hasSucceed = false;
            var model = _db.ApplicationSettings.FirstOrDefault(x => x.Id == Vmodel.ID);
            model.AppName = Vmodel.AppName;
            if (Logo != null)
                model.Logo = CustomSerializer.Serialize(Logo);
            if (Favicon != null)
                model.Favicon = CustomSerializer.Serialize(Favicon);
            model.Address = Vmodel.Address;
            model.PhoneNumber = Vmodel.PhoneNumber;
            model.Email = Vmodel.Email;
            model.InstagramHandle = Vmodel.InstagramHandle;
            model.FacebookHandle = Vmodel.FacebookHandle;
            model.TwitterHandle = Vmodel.TwitterHandle;
            model.LinkedInHandle = Vmodel.LinkedInHandle;
            model.AfflilateBookBonus = CustomSerializer.UnMaskString(Vmodel.AfflilateBookBonusString);
            model.AfflilateAuthorBonus = CustomSerializer.UnMaskString(Vmodel.AfflilateAuthorBonusString);
            model.AfflilateArticleBonus = CustomSerializer.UnMaskString(Vmodel.AfflilateArticleBonusString);
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            var state = _db.SaveChanges();
            if (state > 0)
            {
                hasSucceed = true;
            }
            return hasSucceed;
        }

        public List<CategoriesVM> GetCategories()
        {
            var model = _db.Categories.Where(x => x.IsDeleted == false && x.ParentID == null).Select(b => new CategoriesVM()
            {
                Id = b.Id,
                Description = b.Description,
                ContentInformation = b.ContentInformation,
            }).ToList();
            return model;
        }
        public bool CreateCategory(CategoriesVM Vmodel)
        {
            bool hasSaved = false;
            Categories model = new Categories()
            {
                Description = Vmodel.Description,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                ContentInformation = Vmodel.ContentInformation,
            };
            _db.Categories.Add(model);
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public CategoriesVM GetCategory(int ID)
        {
            var model = _db.Categories.Where(x => x.Id == ID).Select(b => new CategoriesVM()
            {
                Id = b.Id,
                Description = b.Description,
                ContentInformation = b.ContentInformation,
            }).FirstOrDefault();
            return model;
        }
        public bool EditCategory(CategoriesVM Vmodel)
        {
            bool hasSaved = false;
            Categories model = _db.Categories.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Description = Vmodel.Description;
            model.ContentInformation = Vmodel.ContentInformation;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public bool DeleteCategory(int ID)
        {
            bool hasSaved = false;
            var model = _db.Categories.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }

        // Sub categories
        public List<CategoriesVM> GetSubCategories()
        {
            var model = _db.Categories.Where(x => x.IsDeleted == false && x.ParentID != null).Select(b => new CategoriesVM()
            {
                Id = b.Id,
                Description = b.Description,
                ContentInformation = b.ContentInformation,
                Parent = b.Parent.Description,
                ParentID = b.ParentID
            }).ToList();
            return model;
        }
        public bool CreateSubCategory(CategoriesVM Vmodel)
        {
            bool hasSaved = false;
            Categories model = new Categories()
            {
                Description = Vmodel.Description,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                ContentInformation = Vmodel.ContentInformation,
                ParentID = Vmodel.ParentID,
                ParentDescription = _db.Categories.FirstOrDefault(x=>x.Id == Vmodel.ParentID).Description
            };
            _db.Categories.Add(model);
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public CategoriesVM GetSubCategory(int ID)
        {
            var model = _db.Categories.Where(x => x.Id == ID).Select(b => new CategoriesVM()
            {
                Id = b.Id,
                Description = b.Description,
                ContentInformation = b.ContentInformation,
                ParentID = b.ParentID
            }).FirstOrDefault();
            return model;
        }
        public bool EditSubCategory(CategoriesVM Vmodel)
        {
            bool hasSaved = false;
            Categories model = _db.Categories.FirstOrDefault(x => x.Id == Vmodel.Id);
            model.Description = Vmodel.Description;
            model.ContentInformation = Vmodel.ContentInformation;
            model.ParentID = Vmodel.ParentID;
            model.ParentDescription = _db.Categories.FirstOrDefault(x => x.Id == Vmodel.ParentID).Description;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
        public bool DeleteSubCategory(int ID)
        {
            bool hasSaved = false;
            var model = _db.Categories.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            hasSaved = true;
            return hasSaved;
        }
    }
}