using System.Collections.Generic;
using MagStore.Entities;
using MagStore.Entities.Enums;

namespace MagStore.Web.Models.Product
{
    public class ProductCategoriesViewModel
    {
        public IEnumerable<ProductType> Categories { get; set; }
        public IEnumerable<Entities.Product> Products { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }

        public IDictionary<string, string> Filters { get; set; }
    }
}