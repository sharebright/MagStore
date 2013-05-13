using SagePayMvc;

namespace MagStore.Web.Models.ShoppingCart
{
    public class SaveAddressesPostInputModel
    {
        public Address BillingAddress { get; set; }
        public bool UseBillingAddress { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}