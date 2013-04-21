using System.Collections.Generic;

namespace MagStore.Web.Models.Product
{
    public class ViewProductsByCategoryViewModel
    {
        public IEnumerable<RavenDbMembership.Entities.Product> Products { get; set; }

        public string ProductType { get; set; }
    }
}