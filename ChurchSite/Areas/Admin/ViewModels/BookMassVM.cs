using ChurchSite.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class BookMassVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string For { get; set; }
        public int[] Intensions { get; set; }
        public string SelectedIntensions { get; set; }
        public string SelectedMassess { get; set; }
        public string Other { get; set; }
        public int No_Of_Days { get; set; }
        public decimal Amount { get; set; }
        public string AmountStr { get; set; }
        public int[] Mass { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string command { get; set; }

    }

}
