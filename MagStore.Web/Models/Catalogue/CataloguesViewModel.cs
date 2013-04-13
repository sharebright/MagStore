using System.Collections.Generic;

namespace MagStore.Web.Models.Catalogue
{
    public class CataloguesViewModel
    {
        public IList<RavenDBMembership.Entities.Catalogue> Catalogues { get; set; }

        //        public string Id { get { return catalogue.CatalogueId; } }
//
//        public string Name { get { return catalogue.CatalogueName; } }
//        public decimal DiscountAmount { get { return catalogue.DiscountAmount; } }
//        public DiscountType DiscountType { get { return catalogue.DiscountType; } }
//        public IEnumerable<Product> Products { get { return catalogue.Products; } }
//        public IEnumerable<Promotion> Promotions { get { return catalogue.Promotions; } }
    }
}