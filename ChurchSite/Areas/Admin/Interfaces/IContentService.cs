using ChurchSite.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChurchSite.Areas.Admin.Interfaces
{
    interface IContentService
    {
        List<SacramentVM> GetSacraments();
        bool AddSacrament(SacramentVM vmodel, HttpPostedFileBase photo);
        SacramentVM GetSacrament(int id);
        bool UpdateSacrament(SacramentVM vmodel, HttpPostedFileBase photo);
        void DeleteSacrament(int id);

        List<PrayerVM> GetPrayers();
        bool AddPrayer(PrayerVM vmodel, HttpPostedFileBase photo);
        PrayerVM GetPrayer(int id);
        bool UpdatePrayer(PrayerVM vmodel, HttpPostedFileBase photo);
        void DeletePrayer(int id);

        List<AnnouncementVM> GetAnnouncement();
        bool AddAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase photo);
        AnnouncementVM GetAnnouncement(int id);
        bool UpdateAnnouncement(AnnouncementVM vmodel, HttpPostedFileBase photo);
        void DeleteAnnouncement(int id);

        List<OrganisationVM> GetOrganisations();
        bool AddOrganisation(OrganisationVM vmodel, HttpPostedFileBase photo);
        OrganisationVM GetOrganisation(int id);
        bool UpdateOrganisation(OrganisationVM vmodel, HttpPostedFileBase photo);
        void DeleteOrganisation(int id);

        List<GalleryVM> GetGalleries();

        bool AddGallery(GalleryVM vmodel, HttpPostedFileBase data, string format);

        GalleryVM GetGallery(int id);

        bool UpdateGallery(GalleryVM vmodel, HttpPostedFileBase data, string format);

        bool DeleteGallery(int id);
        bool UpdateHierarchy(int[] data);
    }
}
