using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Prayer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string FilePath { get; set; }
        public byte[] Image { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? CreatedUserID { get; set; }
        public int? EdittedUserID { get; set; }

        [ForeignKey("CreatedUserID")]
        public User CreatedUser { get; set; }
        [ForeignKey("EdittedUserID")]
        public User EdittedUser { get; set; }
    }
}
