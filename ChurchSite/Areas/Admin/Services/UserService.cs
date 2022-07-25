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
    public class UserService : IUserService
    {
        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        public UserService()
        {
            _db = new DatabaseEntities();
        }
        public UserService(DatabaseEntities db)
        {
            _db = db;
        }
        #endregion

        //User
        public List<UserVM> GetUsers()
        {
            var model = _db.Users.Where(x => x.IsDeleted == false).Select(b => new UserVM()
            {
                Id = b.Id,
                Firstname = b.Firstname,
                Lastname = b.Lastname,
                Email = b.Email,
                Address = b.Address,
                PhoneNumber = b.PhoneNumber,
                Role = b.Role.Description,
                Username = b.Username,
                DateCreated = b.DateCreated,
                IsActive = b.IsActive
            }).ToList();
            return model;
        }
        public bool CreateUser(UserVM vmodel)
        {
            bool HasSaved = false;
            User model = new User()
            {
                Username = vmodel.Username,
                Firstname = vmodel.Firstname,
                Lastname = vmodel.Lastname,
                Email = vmodel.Email,
                PhoneNumber = vmodel.PhoneNumber,
                RoleID = vmodel.RoleID,
                Address = vmodel.Address,
                IsActive = true,
                IsDeleted = false,
                Password = CustomEnrypt.Encrypt(vmodel.Password),
                PasswordSalt = CustomEnrypt.Encrypt(vmodel.PasswordSalt),
                DateCreated = DateTime.Now,
            };
            _db.Users.Add(model);
            _db.SaveChanges();
            HasSaved = true;
            return HasSaved;
        }
        public UserVM GetUser(int ID)
        {
            var model = _db.Users.Where(x => x.Id == ID).Select(b => new UserVM()
            {
                Id = b.Id,
                Firstname = b.Firstname,
                Lastname = b.Lastname,
                Email = b.Email,
                Username = b.Username,
                PhoneNumber = b.PhoneNumber,
                Address = b.Address,
                RoleID = b.RoleID,
            }).FirstOrDefault();
            return model;
        }
        public bool EditUser(UserVM vmodel)
        {
            bool HasSaved = false;
            var model = _db.Users.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Firstname = vmodel.Firstname;
            model.Lastname = vmodel.Lastname;
            model.Email = vmodel.Email;
            model.PhoneNumber = vmodel.PhoneNumber;
            model.Address = vmodel.Address;
            model.RoleID = vmodel.RoleID;
            model.DateModified = DateTime.Now;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasSaved = true;
            return HasSaved;
        }
        public bool DeleteUser(int ID)
        {
            bool HasDeleted = false;
            var model = _db.Users.FirstOrDefault(x => x.Id == ID);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasDeleted = true;
            return HasDeleted;
        }
        public bool DeactivateUser(int ID)
        {
            bool HasDeleted = false;
            var model = _db.Users.FirstOrDefault(x => x.Id == ID);
            model.IsActive = false;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasDeleted = true;
            return HasDeleted;
        }
        public bool ActivateUser(int ID)
        {
            bool HasDeleted = false;
            var model = _db.Users.FirstOrDefault(x => x.Id == ID);
            model.IsActive = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasDeleted = true;
            return HasDeleted;
        }

        //Role
        public List<RoleVM> GetRoles()
        {
            var model = _db.Roles.Where(x => x.IsDeleted == false).Select(b => new RoleVM()
            {
                Id = b.Id,
                Description = b.Description
            }).ToList();
            return model;
        }
        public bool CreateRole(RoleVM vmodel)
        {
            bool HasSaved = false;
            Role role = new Role()
            {
                Description = vmodel.Description,
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            _db.Roles.Add(role);
            _db.SaveChanges();
            HasSaved = true;
            return HasSaved;
        }
        public RoleVM GetRole(int ID)
        {
            var model = _db.Roles.Where(x => x.Id == ID).Select(b => new RoleVM()
            {
                Id = b.Id,
                Description = b.Description
            }).FirstOrDefault();
            return model;
        }
        public bool EditRole(RoleVM vmodel)
        {
            bool HasSaved = false;
            var model = _db.Roles.Where(x => x.Id == vmodel.Id).FirstOrDefault();
            model.Description = vmodel.Description;
            model.DateModified = DateTime.Now;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasSaved = true;
            return HasSaved;
        }
        public bool DeleteRole(int ID)
        {
            bool HasDeleted = false;
            var model = _db.Roles.Where(x => x.Id == ID).FirstOrDefault();
            model.IsDeleted = true;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            HasDeleted = true;
            return HasDeleted;
        }
        public RolePermissionVM GetAssignedPermission(int ID)
        {
            var tableData = new List<RolePermissionList>();
            var RoleName = _db.Roles.Where(x => x.Id == ID).FirstOrDefault().Description;
            var CheckPermissionExists = _db.RolePermissions.Where(x => x.RoleID == ID).Count();
            if (CheckPermissionExists == 0)
            {
                tableData = _db.Permissions.Where(x => x.IsDeleted == false && x.ParentID != null).Select(o => new RolePermissionList()
                {
                    Id = o.Id,
                    RoleID = ID,
                    RoleName = RoleName,
                    PermissionID = o.Id,
                    PermissionName = o.Description,
                    PermissionParentID = o.ParentID,
                    IsAssigned = false
                }).ToList();
            }
            else
            {
                var permission = _db.Permissions.Where(x => x.IsDeleted == false);
                var rolepermission = _db.RolePermissions.Where(x => x.RoleID == ID).Select(o => o.Permission);

                var unavailablePermission = permission.Except(rolepermission);
                foreach (var item in unavailablePermission)
                {
                    var Newpermission = new RolePermission();
                    Newpermission.RoleID = ID;
                    Newpermission.PermissionID = item.Id;
                    Newpermission.PermissionParentID = item.ParentID;
                    Newpermission.IsDeleted = false;
                    Newpermission.IsAssigned = false;
                    Newpermission.DateCreated = DateTime.Now;
                    _db.RolePermissions.Add(Newpermission);
                }
                _db.SaveChanges();
                tableData = _db.RolePermissions.Where(x => x.RoleID == ID && x.PermissionParentID != null).Select(o => new RolePermissionList()
                {
                    Id = o.Id,
                    RoleID = ID,
                    RoleName = o.Role.Description,
                    PermissionID = o.Permission.Id,
                    PermissionName = o.Permission.Description,
                    PermissionParentID = o.PermissionParentID,
                    IsAssigned = o.IsAssigned == true ? true : false,
                }).ToList();
            }
            var model = new RolePermissionVM(tableData);
            model.RoleName = RoleName;
            return model;
        }

        //Role Permission
        public RolePermission SavePermission(RolePermissionList rolePermissionVM)
        {
            var rolePermission = new RolePermission();
            rolePermission.PermissionID = rolePermissionVM.PermissionID;
            rolePermission.RoleID = rolePermissionVM.RoleID;
            rolePermission.PermissionParentID = rolePermissionVM.PermissionParentID;
            rolePermission.IsAssigned = rolePermissionVM.IsAssigned;
            rolePermission.IsDeleted = false;
            rolePermission.DateCreated = DateTime.Now;
            _db.RolePermissions.Add(rolePermission);
            _db.SaveChanges();
            return rolePermission;
        }
        public void AssignPermission(RolePermissionList rolePermissionVM)
        {
            var RolePermission = _db.RolePermissions.Where(p => p.RoleID == rolePermissionVM.RoleID && p.PermissionID == rolePermissionVM.PermissionID).FirstOrDefault();
            RolePermission.IsAssigned = rolePermissionVM.IsAssigned;
            _db.Entry(RolePermission).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
        public int CheckPermissionExist(int RoleID, int PermissionID)
        {
            int result;
            result = _db.RolePermissions.Where(x => x.RoleID == RoleID && x.PermissionID == PermissionID).Count();
            return result;
        }
        //Account
        public List<User> CheckCreditials(UserVM userVM)
        {
            var ecryptedPassword = CustomEnrypt.Encrypt(userVM.Password);
            var user = _db.Users.Where(x => x.Username == userVM.Username && x.Password == ecryptedPassword && x.IsDeleted == false && x.IsActive == true).ToList();
            return user;
        }

        public List<AfflilateBonusVM> GetAfflilate_Bouns()
        {
            var afflilates = _db.Users.Where(x => x.Role.Description == "Afflilate" && x.IsActive == true && x.IsDeleted == false).Select(b => new AfflilateBonusVM()
            {
                AfflilateUserID = b.Id,
                AfflilateUser = b.Firstname + " " + b.Lastname,
                afflilateEmail = b.Email
            }).ToList();

            foreach (var afflilate in afflilates)
            {
                var afflilatetotalbonus = _db.AfflilateBonusManagers.Where(x => x.AfflilateUserID == afflilate.AfflilateUserID).ToList();
                if (afflilatetotalbonus.Count() > 0)
                    afflilate.TotalAmount = afflilatetotalbonus.Sum(x => x.Amount);
                else
                    afflilate.TotalAmount = 0.00m;

                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                afflilate.TotalAmountString = afflilate.TotalAmount.ToString("N", nfi);

                var afflilateBonuses = new List<Bonus>();
                var bonusTypes = Enum.GetValues(typeof(BonusType)).Cast<BonusType>().Select(value => value.ToString()).ToList();
                foreach(var bonusType in bonusTypes)
                {
                    var bonusval = (BonusType)Enum.Parse(typeof(BonusType), bonusType);
                    var afflilatebonus = _db.AfflilateBonusManagers.FirstOrDefault(x => x.AfflilateUserID == afflilate.AfflilateUserID && x.BonusType == bonusval);
                    var bonus = new Bonus();
                    bonus.BonusType = bonusType;
                    bonus.Amount = afflilatebonus == null ? 0 : afflilatebonus.Amount;
                    bonus.AmountString = bonus.Amount.ToString("N", nfi);

                    afflilateBonuses.Add(bonus);
                }
                afflilate.Bonus = afflilateBonuses;
            }
            return afflilates;
        }
        public List<ArticleVM> GetArticleAddedByAfflilate(int AfflilateUserID)
        {
            byte[] empty = { 4, 3 };
            var model = _db.Articles.Where(x => x.CreatedByID == AfflilateUserID && x.IsDeleted == false).Select(b => new ArticleVM()
            {
                Id = b.Id,
                Description = b.Description,
                Image = b.Image == null ? empty : b.Image,
                DateCreated = b.DateCreated,
            }).OrderByDescending(x => x.DateCreated).ToList();
            return model;
        }

        public List<SpiritualistVM> GetSpiritualists()
        {
            byte[] emptyArr = { 4, 3 };
            var model = _db.Spiritualists.Where(x => x.IsDeleted == false).Select(b => new SpiritualistVM()
            {
                Id = b.Id,
                Name = b.Name,
                Role = b.Role,
                Hierarchy = b.Hierarchy,
                FilePath = b.FilePath,
              //  Photo = b.Photo == null ? emptyArr : b.Photo
            }).OrderBy(x => x.Hierarchy).ToList();
            return model;
        }
        public bool AddSpiritualist(SpiritualistVM vmodel, HttpPostedFileBase image)
        {
            var model = new Spiritualist()
            {
                Name = vmodel.Name,
                Role = vmodel.Role,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            //if(image != null)
            //{
            //    model.Photo = CustomSerializer.Serialize(image);
            //}
            if (image != null)
            {
                string filename = image.FileName;
                string path = "/Front_Content/img/CKP/Spiritualist/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                image.SaveAs(targetpath + filename);
            }

            _db.Spiritualists.Add(model);
            _db.SaveChanges();

            return true;
        }
        public SpiritualistVM GetSpiritualist(int id)
        {
            byte[] emptyArr = { 4, 3 };

            var model = _db.Spiritualists.Where(x => x.Id == id).Select(b => new SpiritualistVM()
            {
                Id = b.Id,
                Name = b.Name,
                Role = b.Role,
                FilePath = b.FilePath,
              //  Photo = b.Photo == null ? emptyArr : b.Photo
            }).FirstOrDefault();
           // model.ImageString = Convert.ToBase64String(model.Photo);
            return model;
        }
        public bool UpdateSpiritualist(SpiritualistVM vmodel, HttpPostedFileBase image)
        {
            var model = _db.Spiritualists.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Name = vmodel.Name;
            model.Role = vmodel.Role;
            model.DateModified = DateTime.Now;
            model.Photo = null;
            //if (image != null)
            //    model.Photo = CustomSerializer.Serialize(image);
            if (image != null)
            {
                string filename = image.FileName;
                string path = "/Front_Content/img/CKP/Spiritualist/";
                string targetpath = HttpContext.Current.Server.MapPath("~" + path);
                model.FilePath = path + filename;
                image.SaveAs(targetpath + filename);
            }
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
        public bool DeleteSpiritualist(int id)
        {
            var model = _db.Spiritualists.FirstOrDefault(x => x.Id == id);
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
                    var model = _db.Spiritualists.FirstOrDefault(x => x.Id == id);
                    model.Hierarchy = preference;
                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    preference++;
                }
                return true;
            }
            else return false;
        }
        public bool ChechIfMemberExist(MemberVM vmodel)
        {
            return _db.Members.Any(x => x.Email == vmodel.Email || x.PhoneNumber == vmodel.PhoneNumber);
        }
        public bool CreateMember(MemberVM vmodel)
        {
            bool HasSaved = false;
            Member model = new Member()
            {
                Firstname = vmodel.Firstname,
                Lastname = vmodel.Lastname,
                Email = vmodel.Email,
                Gender = vmodel.Gender,
                PhoneNumber = vmodel.PhoneNumber,
                Address = vmodel.Address,
                IsActive = true,
                IsDeleted = false,
                DateCreated = DateTime.Now,
            };
            _db.Members.Add(model);
            _db.SaveChanges();
            HasSaved = true;
            return HasSaved;
        }
    }
}