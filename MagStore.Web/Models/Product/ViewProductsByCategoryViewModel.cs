using System.Collections.Generic;
using RavenDbMembership.Entities;

namespace MagStore.Web.Models.Product
{
    public class ViewProductsByCategoryViewModel
    {
        public IEnumerable<RavenDbMembership.Entities.Product> Products { get; set; }

        public string ProductType { get; set; }

        public IEnumerable<ProductImage> Images { get; set; }

        public IDictionary<string, string> Filters { get; set; } 
    }
}