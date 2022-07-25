using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Helpers
{
    public class PaymentConfigProperties
    {
        public PaymentConfigProperties()
        {
            NameValueCollection section = ConfigurationManager.AppSettings;
            MonnifyBaseUrl = section["MonnifyBaseUrl"];
            MonnifyCreateInvoiceUrl = section["MonnifyCreateInvoiceUrl"];
            MonnifyApiKey = section["MonnifyApiKey"];
            MonnifyClientSecret = section["MonnifyClientSecret"];
            MonnifyAuthentication = section["MonnifyAuthentication"];
            MonnifyContractCode = section["MonnifyContractCode"];
            MonnifyTransactionStatus = section["MonnifyTransactionStatus"];

        }

        public string MonnifyBaseUrl { get; set; }
        public string MonnifyCreateInvoiceUrl { get; set; }
        public string MonnifyApiKey { get; set; }
        public string MonnifyClientSecret { get; set; }
        public string MonnifyAuthentication { get; set; }
        public string MonnifyContractCode { get; set; }
        public string MonnifyTransactionStatus { get; set; }

    }
}