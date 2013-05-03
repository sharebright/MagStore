using System.Collections.Generic;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class Cart : IRavenEntity
    {
        public Cart()
        {
            Products = new List<Product>();
            Promotions = new List<string>();
        }

        public string Id { get; set; }
        public IList<Product> Products { get; set; }
        public IList<string> Promotions { get; set; }
    }
}