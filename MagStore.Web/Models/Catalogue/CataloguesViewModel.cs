using System.Collections.Generic;

namespace MagStore.Web.Models.Catalogue
{
    public class CataloguesViewModel
    {
        public IList<RavenDbMembership.Entities.Catalogue> Catalogues { get; set; }

        //        public string Id { get { return catalogue.Id; } }
//
//        public string Name { get { return catalogue.Name; } }
//        public decimal DiscountAmount { get { return catalogue.DiscountAmount; } }
//        public DiscountType DiscountType { get { return catalogue.DiscountType; } }
//        public IEnumerable<Product> Products { get { return catalogue.Products; } }
//        public IEnumerable<Promotion> Promotions { get { return catalogue.Promotions; } }
    }
}