using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class DonationStorage
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderReason { get; set; }
        public string FeeDescription { get; set; }
        public decimal AmountToPaid { get; set; }
        public string InvoiceNumber { get; set; }
        public bool HasPaid { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public string RawPaymentDate { get; set; }
        public string CheckOutURL { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime InvoiceExpiryDate { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
