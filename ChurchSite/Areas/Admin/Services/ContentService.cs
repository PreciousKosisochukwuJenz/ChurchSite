using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using eLibrarySystem.Areas.Admin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.Services
{
    public class ContentService : IContentService
    {
        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        public ContentService()
        {
            _db = new DatabaseEntities();
        }
        public ContentService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion
        byte[] empty = { 4, 3 };

        public List<SacramentVM> GetSacraments()
        {
            var model = _db.Sacraments.Where(x => x.IsDeleted == false).Select(b => new SacramentVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
                
            }).ToList();
            return model;
        }
        public bool AddSacrament(SacramentVM vmodel, HttpPostedFileBase photo)
        {
            var model = new Sacrament()
            {
                Title = vmodel.Title,
                Description = vmodel.Description,
                Body = vmodel.Body,
                IsDeleted = false,
                DateCreated = DateTime.Now,
                CreatedUserID = Global.AuthenticatedUserID,
            };
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/Sacrament/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path;
                photo.SaveAs(targetpath + filename);
            }

            _db.Sacraments.Add(model);
            _db.SaveChanges();

            return true;
        }
        public SacramentVM GetSacrament(int id)
        {
            var model = _db.Sacraments.Where(x => x.Id == id).Select(b => new SacramentVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Body = b.Body,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).FirstOrDefault();
            return model;
        }
        public bool UpdateSacrament(SacramentVM vmodel, HttpPostedFileBase photo)
        {
            var model = _db.Sacraments.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Title = vmodel.Title;
            model.Description = vmodel.Description;
            model.Body = vmodel.Body;
            model.EdittedUserID = Global.AuthenticatedUserID;
            model.DateModified = DateTime.Now;
            model.Image = null;
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/Sacrament/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return true;
        }
        public void DeleteSacrament(int id)
        {
            var model = _db.Sacraments.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public List<PrayerVM> GetPrayers()
        {
            var model = _db.Prayers.Where(x => x.IsDeleted == false).Select(b => new PrayerVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).ToList();
            return model;
        }
        public bool AddPrayer(PrayerVM vmodel, HttpPostedFileBase photo)
        {
            var model = new Prayer()
            {
                Title = vmodel.Title,
                Description = vmodel.Description,
                Body = vmodel.Body,
                IsDeleted = false,
                DateCreated = DateTime.Now,
                CreatedUserID = Global.AuthenticatedUserID,
            };
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);
            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/PRAYERS/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }
            _db.Prayers.Add(model);
            _db.SaveChanges();

            return true;
        }
        public PrayerVM GetPrayer(int id)
        {
            var model = _db.Prayers.Where(x => x.Id == id).Select(b => new PrayerVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Body = b.Body,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).FirstOrDefault();
            return model;
        }
        public bool UpdatePrayer(PrayerVM vmodel, HttpPostedFileBase photo)
        {
            var model = _db.Prayers.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Title = vmodel.Title;
            model.Description = vmodel.Description;
            model.Body = vmodel.Body;
            model.EdittedUserID = Global.AuthenticatedUserID;
            model.DateModified = DateTime.Now;
            model.Image = null;
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/PRAYERS/";
                string targetpath = HttpContext.Current.Server.MapPath("~"+path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return true;
        }
        public void DeletePrayer(int id)
        {
            var model = _db.Prayers.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public List<AnnouncementVM> GetAnnouncement()
        {
            var model = _db.Announcements.Where(x => x.IsDeleted == false).Select(b => new AnnouncementVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).ToList();
            return model;
        }
        public bool AddAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase photo)
        {
            var model = new Announcement()
            {
                Title = vmodel.Title,
                Description = vmodel.Description,
                Body = vmodel.Body,
                IsDeleted = false,
                DateCreated = DateTime.Now,
                CreatedUserID = Global.AuthenticatedUserID,
            };
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/ANNOUNCEMENT/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }
            _db.Announcements.Add(model);
            _db.SaveChanges();

            return true;
        }
        public AnnouncementVM GetAnnouncement(int id)
        {
            var model = _db.Announcements.Where(x => x.Id == id).Select(b => new AnnouncementVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Body = b.Body,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).FirstOrDefault();
            return model;
        }
        public bool UpdateAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase photo)
        {
            var model = _db.Announcements.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Title = vmodel.Title;
            model.Description = vmodel.Description;
            model.Body = vmodel.Body;
            model.EdittedUserID = Global.AuthenticatedUserID;
            model.DateModified = DateTime.Now;
            model.Image = null;
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/ANNOUNCEMENT/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return true;
        }
        public void DeleteAnnouncement(int id)
        {
            var model = _db.Announcements.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public List<OrganisationVM> GetOrganisations()
        {
            var model = _db.Organisations.Where(x => x.IsDeleted == false).Select(b => new OrganisationVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).ToList();
            return model;
        }
        public bool AddOrganisation(OrganisationVM vmodel, HttpPostedFileBase photo)
        {
            var model = new Organisation()
            {
                Title = vmodel.Title,
                Description = vmodel.Description,
                Body = vmodel.Body,
                IsDeleted = false,
                DateCreated = DateTime.Now,
                CreatedUserID = Global.AuthenticatedUserID,
            };
            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/ORGANISATION/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }
            _db.Organisations.Add(model);
            _db.SaveChanges();

            return true;
        }
        public OrganisationVM GetOrganisation(int id)
        {
            var model = _db.Organisations.Where(x => x.Id == id).Select(b => new OrganisationVM()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Body = b.Body,
                Image = b.Image == null ? empty : b.Image,
                FilePath = b.FilePath,
            }).FirstOrDefault();
            return model;
        }
        public bool UpdateOrganisation(OrganisationVM vmodel,HttpPostedFileBase photo)
        {
            var model = _db.Organisations.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Title = vmodel.Title;
            model.Description = vmodel.Description;
            model.Body = vmodel.Body;
            model.EdittedUserID = Global.AuthenticatedUserID;
            model.DateModified = DateTime.Now;
            model.Image = null;

            //if (photo != null)
            //    model.Image = CustomSerializer.Serialize(photo);

            if (photo != null)
            {
                string filename = photo.FileName;
                string path = "/Front_Content/img/CKP/ORGANISATION/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                photo.SaveAs(targetpath + filename);
            }
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return true;
        }
        public void DeleteOrganisation(int id)
        {
            var model = _db.Organisations.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }


        public List<GalleryVM> GetGalleries()
        {
            var model = _db.Galleries.Where(x => x.IsDeleted == false).Select(b => new GalleryVM()
            {
                Id = b.Id,
                ByteData = b.ByteData,
                Description = b.Description,
                Format = b.Format,
                Category = b.Categories.Description,
                FilePath = b.FilePath,
                Hierarchy = b.Hierarchy
            }).OrderBy(x=>x.Hierarchy).ToList();
            return model;
        }

        public bool AddGallery(GalleryVM vmodel, HttpPostedFileBase data, string format)
        {
            string filename = data.FileName;
            string path = "/Front_Content/img/CKP/GALLERY/";
            string targetpath = HttpContext.Current.Server.MapPath("~" + path);
            var model = new Gallery()
            {
                Description = vmodel.Description,
                Format = format,
                ContentType = data.ContentType,
                FileName = "Video_" + Guid.NewGuid(),
                FilePath = path + filename,
              //  ByteData = CustomSerializer.Serialize(data),
                IsDeleted = false,
                DateCreated = DateTime.Now,
                CategoryID = vmodel.CategoryID,
            };
            _db.Database.CommandTimeout = 5000;
            _db.Galleries.Add(model);
            _db.SaveChanges();
            return true;
        }

        public GalleryVM GetGallery(int id)
        {
            var model = _db.Galleries.Where(x => x.Id == id).Select(b => new GalleryVM()
            {
                Id = b.Id,
                Description = b.Description,
                ByteData = b.ByteData,
                CategoryID = b.CategoryID,
                Category = b.Categories.Description,
                FilePath = b.FilePath,
            }).FirstOrDefault();
            return model;
        }

        public bool UpdateGallery(GalleryVM vmodel, HttpPostedFileBase data, string format)
        {
            var model = _db.Galleries.Where(x => x.Id == vmodel.Id).FirstOrDefault();
            model.Description = vmodel.Description;
            model.CategoryID = vmodel.CategoryID;
            model.LastModified = DateTime.Now;
            model.ByteData = null;
            if (data != null)
            {
              //  model.ByteData = CustomSerializer.Serialize(data);
                model.Format = format;
                model.ContentType = data.ContentType;
                model.FileName = "Video_" + Guid.NewGuid();

                string filename = data.FileName;
                string path = "/Front_Content/img/CKP/GALLERY/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                data.SaveAs(targetpath + filename);
            }
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

        public bool DeleteGallery(int id)
        {
            var model = _db.Galleries.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
        public bool UpdateHierarchy(int[] data)
        {
            if (data != null)
            {
                int preference = 1;
                foreach (var id in data)
                {
                    var model = _db.Galleries.FirstOrDefault(x => x.Id == id);
                    model.Hierarchy = preference;
                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    preference++;
                }
                return true;
            }
            else return false;
        }
    }
}