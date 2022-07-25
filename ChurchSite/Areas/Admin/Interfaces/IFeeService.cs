using ChurchSite.Areas.Admin.ViewModels;
using MonnifyPaymentGateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.Areas.Admin.Interfaces
{
    interface IFeeService
    {
        List<FeeVM> GetFees();
        bool AddFee(FeeVM vmodel);
        FeeVM GetFee(int id);
        bool UpdateFee(FeeVM vmodel);
        bool DeleteFee(int id);

        InvoiceVM CreateInvoice(string redirectURL, DonationVM donationVM);
        int StoreInvoice(Invoice invoice, DonationVM donationVM);
        InvoiceVM UpdateInvoice(Invoice invoice, DonationVM donationVM, int FeeStorageID);
        int StoreBookMassInvoice(Invoice invoice, BookMassVM vmodel);
        InvoiceVM UpdateBookMassInvoice(Invoice invoice, BookMassVM vmodel, int FeeStorageID);

        InvoiceVM CreateBookMassInvoice(string redirectURL, BookMassVM vmodel);

        string UpdateInvoiceByMonnify(MonnifyPaymentNotification payment);
    }
}
