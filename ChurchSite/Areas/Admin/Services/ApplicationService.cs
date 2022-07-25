using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Helpers;
using ChurchSite.DAL.Entity;
using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using eLibrarySystem.Areas.Admin.Helpers;

namespace ChurchSite.Areas.Admin.Services
{
    public class ApplicationService : IApplicationService
    {
        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        public ApplicationService()
        {
            _db = new DatabaseEntities();
        }
        public ApplicationService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion

        public void CreateBaptism(BaptismVM vmodel)
        {
            Baptism model = new Baptism()
            {
                ChristianName = vmodel.ChristianName,
                IgboName = vmodel.IgboName,
                Surname = vmodel.Surname,
                FatherName = vmodel.FatherName,
                MotherName = vmodel.MotherName,
                Town = vmodel.Town,
                Sponsor = vmodel.Sponsor,
                DateOfBaptism = vmodel.DateOfBaptism,
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            _db.Baptisms.Add(model);
            _db.SaveChanges();
        }
        public void CreateEucharist(EucharistVM vmodel)
        {
            Eucharist model = new Eucharist()
            {
                //Firstname = vmodel.Firstname,
                //Lastname = vmodel.Lastname,
                //Email = vmodel.Email,
                //Gender = vmodel.Gender,
                //PhoneNumber = vmodel.PhoneNumber,
                //Address = vmodel.Address,
                //IsActive = true,
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            _db.Eucharists.Add(model);
            _db.SaveChanges();
        }
        public void CreateMatrimony(MatrimonyVM vmodel)
        {
            Matrimony model = new Matrimony()
            {
                Name = vmodel.Name,
                BPT = vmodel.BPT,
                Communicant = vmodel.Communicant,
                Date = vmodel.Date,
                Demonitation = vmodel.Demonitation,
                Phone = vmodel.Phone,
                Residence = vmodel.Residence,
                Wedding_Place = vmodel.Wedding_Place,
                Sponsor = vmodel.Sponsor,
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            _db.Matrimonies.Add(model);
            _db.SaveChanges();
        }
        public BookMassVM CreateBookMass(BookMassVM vmodel)
        {
            BookMass model = new BookMass()
            {
                Name = vmodel.Name,
                For = vmodel.For,
                Other = vmodel.Other,
                No_Of_Days = vmodel.No_Of_Days,
                Amount = CustomSerializer.UnMaskString(vmodel.AmountStr),
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            string selectedMassess = "";
            string selectedIntensions = "";
            foreach (var each in vmodel.Mass)
            {
                var massess = Enum.GetValues(typeof(Mass)).Cast<Mass>().ToList();
                foreach(int mass in massess)
                {
                    if (mass == each) selectedMassess += selectedMassess == "" ? ((Mass)mass).ToString() : String.Concat(", ", ((Mass)mass).ToString());
                }
            }
            foreach (var each in vmodel.Intensions)
            {
                var intensions = Enum.GetValues(typeof(Intensions)).Cast<Intensions>().ToList();
                foreach (int intension in intensions)
                {
                    if (intension == each) selectedIntensions += selectedIntensions == "" ? ((Intensions)intension).ToString() : String.Concat(", ", ((Intensions)intension).ToString());
                }
            }
            model.Mass = selectedMassess;
            model.Intensions = selectedIntensions;
            vmodel.SelectedIntensions = selectedIntensions;
            vmodel.SelectedMassess = selectedMassess;
            vmodel.Id = model.Id;
            _db.BookMasses.Add(model);
            _db.SaveChanges();

            return vmodel;
        }
    }
}