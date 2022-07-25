using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Entities
{
    public class Login:Response
    {
        public LoginResponseBody responseBody { get; set; }
    }
    public class LoginResponseBody 
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
    }
}