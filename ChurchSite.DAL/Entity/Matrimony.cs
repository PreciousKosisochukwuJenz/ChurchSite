using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Matrimony
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Residence { get; set; }
        public string Phone { get; set; }
        public string Demonitation { get; set; }
        public string BPT { get; set; }
        public string Communicant { get; set; }
        public string RegNo { get; set; }
        public DateTime? Date { get; set; }
        public string Wedding_Place { get; set; }
        public string Sponsor { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
