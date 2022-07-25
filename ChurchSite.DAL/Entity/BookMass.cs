using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class BookMass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string For { get; set; }
        public string Intensions { get; set; }
        public string Other { get; set; }
        public int No_Of_Days { get; set; }
        public decimal Amount { get; set; }
        public string Mass { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
