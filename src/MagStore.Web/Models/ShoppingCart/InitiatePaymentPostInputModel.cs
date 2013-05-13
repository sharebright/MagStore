using System.Collections.Generic;

namespace MagStore.Web.Models.ShoppingCart
{
    public class InitiatePaymentPostInputModel
    {
        public IEnumerable<string> Products { get; set; }

        public decimal Total { get; set; }
    }
}