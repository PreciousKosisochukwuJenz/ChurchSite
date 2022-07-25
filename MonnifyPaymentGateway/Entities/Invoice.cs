using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Entities
{

    public class InvoiceResponse : Response
    {
        public Invoice ResponseBody { get; set; }

    }
    public class Invoice
    {
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
        public IList<IncomeSplitConfig> IncomeSplitConfig { get; set; }
        public string RedirectUrl { get; set; }
        public byte[] InvoiceLogo { get; set; }
        public IList<dynamic> InvoiceItems { get; set; }
        public string paymentStatus { get; set; }

    }
    public class IncomeSplitConfig
    {
        public string subAccountCode { get; set; }
        public double feePercentage { get; set; }
        public double splitPercentage { get; set; }
        public decimal SplitAmount { get; set; }
        public bool feeBearer { get; set; }

    }
}

