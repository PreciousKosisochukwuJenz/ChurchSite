using MonnifyPaymentGateway.Core.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Core
{
    public class HttpClient:IHttpClient
    {
        public IRestResponse Execute(IEnvironment Environment, RestRequest Request, RestClient Client)
        {
            //var client = new RestClient(Environment.resourceUrl);
            //Request.RequestFormat = DataFormat.Json;
            var response = Client.Execute(Request);
            return response;
        }
    }
}