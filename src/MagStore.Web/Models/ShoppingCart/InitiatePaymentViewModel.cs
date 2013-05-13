using SagePayMvc;

namespace MagStore.Web.Models.ShoppingCart
{
    public class InitiatePaymentViewModel
    {
        public Address BillingAddress { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}