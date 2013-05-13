using System.Collections.Generic;

namespace MagStore.Web.Models.ShoppingCart
{
    public class CheckoutViewModel
    {
        public IList<Entities.Product> Products { get; set; }

        public decimal SubTotal { get; set; }

        public decimal DeliveryCharge { get; set; }
    }
}