using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MonnifyPaymentGateway.Helpers
{
    public class Encrypt
    {
        public string SHA512(string input)
        {
            
            byte[] hash;
            var data = Encoding.UTF8.GetBytes(input);
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }

            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}