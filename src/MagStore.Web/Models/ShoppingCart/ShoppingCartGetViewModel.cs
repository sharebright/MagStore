using System.Collections.Generic;
using MagStore.Entities;

namespace MagStore.Web.Models.ShoppingCart
{
    public class ShoppingCartGetViewModel
    {
        public Cart Cart { get; set; }

        public IEnumerable<Entities.Product> Products { get; set; }

        public string Quantity { get; set; }
    }
}