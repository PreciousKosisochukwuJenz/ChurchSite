using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.DAL.Entity
{
    public class CustomMail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateRecieved { get; set; }
        public bool HasRead { get; set; }
    }
}