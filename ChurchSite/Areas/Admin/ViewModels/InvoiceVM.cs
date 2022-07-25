using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.ViewModels
{
    public class InvoiceVM
    {
        public int storageID { get; set; }
        public int FeeId { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceReference { get; set; }
        public string Description { get; set; }
        public string BankCode { get; set; }
        public string ContractCode { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerUniqueId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string CheckoutUrl { get; set; }
        public string CreatedBy { get; set; }
        public string InvoiceStatus { get; set; }
        public string CreatedOn { get; set; }
        public string ExpiryDate { get; set; }
        public string CurrencyCode { get; set; }
        public string[] PaymentMethods { get; set; }
        public string RedirectUrl { get; set; }
        public byte[] InvoiceLogo { get; set; }
        public IList<dynamic> InvoiceItems { get; set; }
        public string paymentStatus { get; set; }
        public string Message { get; set; }
        public string FeeDescription { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string DateGenerated { get; set; }
        public string InvoiceNumber { get; set; }
        public string AmountToPaid { get; set; }
        public DateTime InvoiceExpDate { get; set; }
        public DateTime DateGene { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string InvoiceExpiryDate { get; set; }

    }
}