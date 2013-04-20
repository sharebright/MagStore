using System.Collections.Generic;
using RavenDbMembership.Entities.Enums;

namespace MagStore.Web.Models.Catalogue
{
    public class CreateCatalogueInputModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RavenDbMembership.Entities.Product> Products { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<RavenDbMembership.Entities.Promotion> Promotions { get; set; }
    }
}