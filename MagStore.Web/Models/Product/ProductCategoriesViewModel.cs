using System.Collections.Generic;
using RavenDbMembership.Entities.Enums;

namespace MagStore.Web.Models.Product
{
    public class ProductCategoriesViewModel
    {
        public IEnumerable<ProductType> Categories { get; set; }
        public IEnumerable<RavenDbMembership.Entities.Product> Products { get; set; }
        public IEnumerable<RavenDbMembership.Entities.ProductImage> Images { get; set; }

        public IDictionary<string, string> Filters { get; set; }
    }
}