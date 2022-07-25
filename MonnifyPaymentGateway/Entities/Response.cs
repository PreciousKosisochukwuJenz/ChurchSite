using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Entities

{
    public class Response
    {
        public bool RequestSuccessful { get; set; }
        public string ResponseMessage { get; set; }
        public string responseCode { get; set; }
    }
}