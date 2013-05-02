using System.Collections.Generic;
using MagStore.Entities;

namespace MagStore.Web.Models.Product
{
    public class ViewProductsByCategoryViewModel
    {
        public IEnumerable<Entities.Product> Products { get; set; }

        public string ProductType { get; set; }

        public IEnumerable<ProductImage> Images { get; set; }

        public IDictionary<string, string> Filters { get; set; } 
    }
}