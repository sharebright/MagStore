using System.Collections.Generic;

namespace MagStore.Web.Models.Product
{
    public class ProductsViewModel
    {
        public RavenDbMembership.Entities.Catalogue Catalogue { get; set; } 
        public IEnumerable<RavenDbMembership.Entities.Product> Products { get; set; }
    }
}