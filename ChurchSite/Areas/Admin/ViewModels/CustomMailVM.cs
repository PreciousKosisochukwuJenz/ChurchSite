using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class CustomMailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime DateRecieved { get; set; }
    }
}