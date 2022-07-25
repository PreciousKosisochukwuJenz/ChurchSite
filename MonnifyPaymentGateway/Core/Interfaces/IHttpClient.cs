using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Core.Interfaces
{
    public interface IHttpClient
    {
        IRestResponse Execute(IEnvironment Environment, RestRequest Request, RestClient Client);
    }
}