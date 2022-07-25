using ChurchSite.DAL.Helpers;
using ChurchSite.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchSite.DAL.DataConnection;

namespace ChurchSite.DAL.DataConnection
{
    public class Seed
    {
        public static void DatabaseSeed(DatabaseEntities context)
        {
            context.ApplicationSettings.AddOrUpdate(x => x.Id,
          new Entity.ApplicationSettings()
          {
              Id = 1,
              AppName = "Christ the King Parish",
          });
            context.Roles.AddOrUpdate(x => x.Id,
         new Entity.Role()
         {
             Id = 1,
             Description = "Super administrator",
             IsDeleted = false,
             DateCreated = DateTime.Now
         });
            context.Permissions.AddOrUpdate(x => x.Id,
     new Entity.Permission()
     {
         Id = 1,
         Description = "Settings module",
         IsDeleted = false,
         DateCreated = DateTime.Now
     },
      new Entity.Permission()
      {
          Id = 2,
          Description = "User module",
          IsDeleted = false,
          DateCreated = DateTime.Now
      },
       new Entity.Permission()
       {
           Id = 3,
           Description = "Application settings",
           IsDeleted = false,
           DateCreated = DateTime.Now,
           ParentID = 1
       },
       new Entity.Permission()
       {
           Id = 4,
           Description = "Manage roles",
           IsDeleted = false,
           DateCreated = DateTime.Now,
           ParentID = 2
       },
       new Entity.Permission()
       {
           Id = 5,
           Description = "Manage users",
           IsDeleted = false,
           DateCreated = DateTime.Now,
           ParentID = 2
       },
       new Entity.Permission()
       {
           Id = 6,
           Description = "Manage categories",
           IsDeleted = false,
           DateCreated = DateTime.Now,
           ParentID = 1
       },
       new Entity.Permission()
       {
           Id = 7,
           Description = "Article module",
           IsDeleted = false,
           DateCreated = DateTime.Now,
       },
       new Entity.Permission()
       {
           Id = 8,
           Description = "Manage articles",
           IsDeleted = false,
           DateCreated = DateTime.Now,
           ParentID = 7
       },
             new Entity.Permission()
             {
                 Id = 9,
                 Description = "Dashboard module",
                 IsDeleted = false,
                 DateCreated = DateTime.Now,
             },
             new Entity.Permission()
             {
                 Id = 10,
                 Description = "Analytics",
                 IsDeleted = false,
                 DateCreated = DateTime.Now,
                 ParentID = 9
             },
                new Entity.Permission()
                {
                    Id = 11,
                    Description = "Manage Spiritualist",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    ParentID = 2
                }
                ,
                new Entity.Permission()
                {
                    Id = 12,
                    Description = "Prayer module",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                }
                ,
                new Entity.Permission()
                {
                    Id = 13,
                    Description = "Sacrament module",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                }
                ,
                new Entity.Permission()
                {
                    Id = 14,
                    Description = "Organisation module",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                }
                  ,
                new Entity.Permission()
                {
                    Id = 15,
                    Description = "Manage prayers",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    ParentID = 12
                },
                new Entity.Permission()
                {
                    Id = 16,
                    Description = "Manage sacraments",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    ParentID = 13
                },
                new Entity.Permission()
                {
                    Id = 17,
                    Description = "Manage organisations",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    ParentID = 14
                },
                new Entity.Permission()
                {
                    Id = 18,
                    Description = "Gallery module",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                },
                new Entity.Permission()
                {
                    Id = 19,
                    Description = "Manage galleries",
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    ParentID = 18
                },
                  new Entity.Permission()
                  {
                      Id = 20,
                      Description = "Manage module",
                      IsDeleted = false,
                      DateCreated = DateTime.Now,
                  },
                   new Entity.Permission()
                   {
                       Id = 21,
                       Description = "Manage Announcement",
                       IsDeleted = false,
                       DateCreated = DateTime.Now,
                       ParentID = 20
                   }
          );
            context.Users.AddOrUpdate(x => x.Id,
  new Entity.User()
  {
      Id = 1,
      Firstname = "Kosisochukwu",
      Lastname = "Jenz",
      Email = "kcokolo10@gmail.com",
      Address = "St. Theresa's parish",
      PhoneNumber = "09056893344",
      Username = "Administrator",
      Password = CustomEnrypt.Encrypt("Legendary"),
      PasswordSalt = CustomEnrypt.Encrypt("Legendary"),
      RoleID = 1,
      IsActive = true,
      IsDeleted = false,
      DateCreated = DateTime.Now
  });
            context.RolePermissions.AddOrUpdate(x => x.Id,
                new Entity.RolePermission()
                {
                    Id = 1,
                    RoleID = 1,
                    PermissionID = 4,
                    PermissionParentID = 2,
                    IsAssigned = true,
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                });

            //Save all changes
            context.SaveChanges();
        }
    }
}
