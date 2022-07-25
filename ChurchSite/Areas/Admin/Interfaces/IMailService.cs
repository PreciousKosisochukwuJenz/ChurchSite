using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.Areas.Admin.Interfaces
{
    interface IMailService
    {
        List<CustomMailVM> GetAllMails();
        List<CustomMailVM> GetNotReadMails();
        CustomMailVM GetMailForReply(int id);
        bool DeleteMail(int id);
        bool MarkMailAsRead(int id);
    }
}
