using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.Services
{
    public class MailService : IMailService
    {
        #region Instanciation
        readonly DatabaseEntities _db;
        public MailService()
        {
            _db = new DatabaseEntities();
        }
        public MailService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion


        public List<CustomMailVM> GetAllMails()
        {
            var model = _db.CustomMails.Where(x=>x.IsDeleted == false && x.HasRead == true).Select(b => new CustomMailVM()
            {
                Id = b.Id,
                Subject = b.Subject,
                Email = b.Email,
                Name = b.Name,
                Message = b.Message,
                DateRecieved = b.DateRecieved
            }).ToList();
            return model;
        }
        public List<CustomMailVM> GetNotReadMails()
        {
            var model = _db.CustomMails.Where(x => x.IsDeleted == false && x.HasRead == false).Select(b => new CustomMailVM()
            {
                Id = b.Id,
                Subject = b.Subject,
                Email = b.Email,
                Name = b.Name,
                Message = b.Message,
                DateRecieved = b.DateRecieved
            }).ToList();
            return model;
        }
        public CustomMailVM GetMailForReply(int id)
        {
            var model = _db.CustomMails.Where(x => x.Id == id).Select(b => new CustomMailVM()
            {
                Id = b.Id,
                Subject = b.Subject,
                Email = b.Email,
                Name = b.Name,
                Message = b.Message,
                DateRecieved = b.DateRecieved
            }).FirstOrDefault();
            return model;
        }
        public bool DeleteMail(int id)
        {
            var model = _db.CustomMails.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
        public bool MarkMailAsRead(int id)
        {
            var model = _db.CustomMails.FirstOrDefault(x => x.Id == id);
            model.HasRead = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

    }
}