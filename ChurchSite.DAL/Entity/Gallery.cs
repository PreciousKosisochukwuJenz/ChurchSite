using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Gallery
    {
        public int Id { get; set; }
        public int? CategoryID { get; set; }
        public byte[] ByteData { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? Hierarchy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }

        [ForeignKey("CategoryID")]
        public Categories Categories { get; set; }
    }
}
