using ChurchSite.DAL.Entity;
using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChurchSite.Areas.Admin.Interfaces
{
   public interface IApplicationService
    {
        void CreateBaptism(BaptismVM vmodel);
        void CreateEucharist(EucharistVM vmodel);
        void CreateMatrimony(MatrimonyVM vmodel);
        BookMassVM CreateBookMass(BookMassVM vmodel);
    }
}
