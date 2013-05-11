using System.Collections.Generic;
using System.Web.Routing;
using MagStore.Entities;
using SagePayMvc;

namespace MagStore.Payments.Messages
{
    public interface IAuthRequest
    {
        RequestContext Context { get; set; }
        string TransactionId { get; set; }
        IList<Product> Products { get; set; }
        string CustomerEmail { get; set; }
        Address BillingAddress { get; set; }
        Address DeliveryAddress { get; set; }
    }
}