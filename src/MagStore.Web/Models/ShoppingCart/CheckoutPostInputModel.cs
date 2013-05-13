using System.Collections.Generic;

namespace MagStore.Web.Models.ShoppingCart
{
    public class CheckoutPostInputModel
    {
        public IEnumerable<string> Products { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal SubTotal { get; set; }
    }
}