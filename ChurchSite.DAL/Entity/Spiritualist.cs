using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Spiritualist
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public int? Hierarchy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
