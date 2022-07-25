using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Entities
{
    public class MonnifyPaymentNotification
    {
        public string transactionReference { get; set; }
        public string paymentReference { get; set; }
        public string amountPaid { get; set; }
        public string totalPayable { get; set; }
        public string paidOn { get; set; }
        public string paymentStatus { get; set; }
        public string paymentDescription { get; set; }
        public string transactionHash { get; set; }
        public string currency { get; set; }
    }
}