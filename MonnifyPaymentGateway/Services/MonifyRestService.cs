using ChurchSite.DAL.Entity;
using MonnifyPaymentGateway.Core;
using MonnifyPaymentGateway.Core.Interfaces;
using MonnifyPaymentGateway.Entities;
using MonnifyPaymentGateway.Helpers;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace MonnifyPaymentGateway.Services
{
    public class MonifyRestService : IMonifyRestService
    {
        private readonly IHttpClient _HttpCLient;
        private readonly IEnvironment _MonnifyEnvironment;

        private string AccessToken;
        public MonifyRestService()
        {
        }
        public MonifyRestService(IHttpClient HttpCLient, IEnvironment MonnifyEnvironment)
        {
            _HttpCLient = HttpCLient;
        }

        public HttpResponseMessage MakeGetServiceRequest(IEnvironment Environment, string authorisationString)
        {
            try
            {
                System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + authorisationString);
                var response = _client.GetAsync(Environment.ResourceUrl()).Result;
                return response;
            }
            catch (Exception ex)
            {
                (new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> " + ex.Message + "\r\n"));
                throw ex;
            }
        }
        public HttpResponseMessage MakePostServiceRequest(IEnvironment Environment, object postParameters, string authorisationString)
        {
            try
            {
                System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + authorisationString);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Environment.ResourceUrl());
                var json = JsonConvert.SerializeObject(postParameters);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _client.SendAsync(request).Result;
                return response;
            }
            catch (Exception ex)
            {
                (new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> " + ex.Message + "\r\n"));
                throw ex;
            }
        }
        public HttpResponseMessage Authenticate(IEnvironment Environment, string authorisationString)
        {
            try
            {
                System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();
                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + authorisationString);

                var response = _client.PostAsync(Environment.ResourceUrl(), null).Result;
                return response;
            }
            catch (Exception ex)
            {
                new Logger().LogMonnify(new Exception(DateTime.Now.ToString() + ">> " + ex.Message + "\r\n"));
                throw ex;
            }
        }
        public InvoiceResponse CreateInvoice(Invoice invoice)
        {
            var environment = new MonnifyEnvironment().InitializeEnvironment((new PaymentConfigProperties()).MonnifyCreateInvoiceUrl);

            var _invoice = new
            {
                amount = invoice.Amount.ToString(),
                invoiceReference = invoice.InvoiceReference,
                description = invoice.Description,
                currencyCode = invoice.CurrencyCode,
                contractCode = environment.ContractCode(),
                customerEmail = invoice.CustomerEmail,
                customerName = invoice.CustomerName,
                expiryDate = invoice.ExpiryDate,
                paymentMethods = invoice.PaymentMethods,
                incomeSplitConfig = invoice.IncomeSplitConfig,
                redirectUrl = invoice.RedirectUrl
            };
            //var authorisationToken = GetAccessToken();
            (new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> Request: " + JsonConvert.SerializeObject(_invoice) + "\r\n"));

            var createInvoiceResponse = MakePostServiceRequest(environment, _invoice, environment.AuthorizationString());

            var response = JsonConvert.DeserializeObject<InvoiceResponse>(createInvoiceResponse.Content.ReadAsStringAsync().Result);

            (new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> Response: " + createInvoiceResponse.Content.ReadAsStringAsync().Result.ToString() + "\r\n"));

            return response;
        }
        public string GetAccessToken()
        {
            var environment = new MonnifyEnvironment().InitializeEnvironment((new PaymentConfigProperties()).MonnifyAuthentication);
            //var loginResponse = Authenticate(environment);
            var loginResponse = Authenticate(environment, environment.AuthorizationString());
            var response = JsonConvert.DeserializeObject<Login>(loginResponse.Content.ReadAsStringAsync().Result);
            return response.responseBody.accessToken;
        }
        public bool isTransactionValid(string transactionReference)
        {
            bool hasPaid = false;
            var resourceUrl = (new PaymentConfigProperties()).MonnifyTransactionStatus.Replace("{TRANSACTION_REFERENCE}", transactionReference);
            var environment = new MonnifyEnvironment().InitializeEnvironment(resourceUrl);
            var transactionStatusResponse = MakeGetServiceRequest(environment, environment.AuthorizationString());
            var response = JsonConvert.DeserializeObject<InvoiceResponse>(transactionStatusResponse.Content.ReadAsStringAsync().Result);

            if (response.ResponseBody != null)
                hasPaid = response.ResponseBody.paymentStatus == "PAID" ? true : false;

            (new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> Validate Payment " + transactionReference + "Has Paid= " + hasPaid.ToString() + "\r\n"));

            return hasPaid;
        }

    }
}