using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class BaptismVM
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string ChristianName { get; set; }
        public string IgboName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Town { get; set; }
        public string RegNo { get; set; }
        public DateTime? DateOfBaptism { get; set; }
        public string Sponsor { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
