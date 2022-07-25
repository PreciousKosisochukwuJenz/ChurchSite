using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Core.Interfaces
{
    public interface IEnvironment
    {
        string BaseUrl();
        string CreateInvoiceUrl();
        string ResourceUrl();
        string AuthorizationString();
        string ApiKey();
        string ContractCode();
        string ClientSecret();
    }
}