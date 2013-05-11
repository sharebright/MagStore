using System.Collections.Generic;
using MagStore.Entities.Enums;

namespace MagStore.Web.Models.Catalogue
{
    public class CreateCatalogueInputModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Entities.Product> Products { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<Entities.Promotion> Promotions { get; set; }
    }
}