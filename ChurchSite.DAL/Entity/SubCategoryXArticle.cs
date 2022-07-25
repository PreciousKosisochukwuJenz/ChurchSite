using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class SubCategoryXArticle
    {
        public int Id { get; set; }
        public int? ArticleID { get; set; }
        public int? SubcategoryID { get; set; }
        public bool IsDeleted { get; set; }

     
        [ForeignKey("SubcategoryID")]
        public Categories SubCategories { get; set; }
     
    }
}
