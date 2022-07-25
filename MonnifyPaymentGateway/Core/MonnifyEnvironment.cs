using MonnifyPaymentGateway.Core.Interfaces;
using MonnifyPaymentGateway.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace MonnifyPaymentGateway.Core
{
   public class MonnifyEnvironment : IEnvironment
    {
        PaymentConfigProperties config = new PaymentConfigProperties();
            
        private string baseUrl;
        private string apiKey;
        private string clientSecret;
        private string webUrl;
        private string createInvoiceUrl;
        private string _resourceUrl;
        private string contractCode;

        public MonnifyEnvironment()
        {
            this.clientSecret = config.MonnifyClientSecret;
        }
        public MonnifyEnvironment InitializeEnvironment(string resourceUrl)
        {
            this.apiKey = config.MonnifyApiKey;
            this.baseUrl = config.MonnifyBaseUrl;
            this.webUrl = config.MonnifyBaseUrl;
            this.createInvoiceUrl = config.MonnifyCreateInvoiceUrl;
            this._resourceUrl = this.baseUrl + resourceUrl;
            this.clientSecret = config.MonnifyClientSecret;
            this.contractCode = config.MonnifyContractCode;
            return this;
        }

        public string BaseUrl()
        {
            return this.baseUrl;
        }
        public string ResourceUrl()
        {
            return this._resourceUrl;
        }
        public string ApiKey()
        {
            return this.apiKey;
        }
        public string ClientSecret()
        {
            return this.clientSecret;
        }
        public string ContractCode()
        {
            return this.contractCode;
        }
        public string CreateInvoiceUrl()
        {
            return this.baseUrl;
        }

        public string AuthorizationString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":" + clientSecret));
        }

        public string WebUrl()
        {
            return this.webUrl;
        }
    }
}