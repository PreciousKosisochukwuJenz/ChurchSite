using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string FilePath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CreatedUserID { get; set; }
        public int? CategoryID { get; set; }
        public int? EdittedUserID { get; set; }
        public int? CreatedByID { get; set; }
        public bool IsFeatured { get; set; }
        public string Title { get; set; }
        public string PublishedBy { get; set; }

        [ForeignKey("CreatedByID")]
        public User CreatedBy { get; set; }
        [ForeignKey("CategoryID")]
        public Categories Category { get; set; }
        [ForeignKey("CreatedUserID")]
        public User CreatedUser { get; set; }
        [ForeignKey("EdittedUserID")]
        public User EdittedUser { get; set; }
    }
}
