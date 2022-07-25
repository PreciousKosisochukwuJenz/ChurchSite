using MonnifyPaymentGateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonnifyPaymentGateway.Services
{
    public interface IMonifyRestService
    {
        InvoiceResponse CreateInvoice(Invoice invoice);
        bool isTransactionValid(string transactionReference);
    }
}