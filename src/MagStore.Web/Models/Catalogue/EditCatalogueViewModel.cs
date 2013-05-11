using System.Collections.Generic;
using MagStore.Entities.Enums;

namespace MagStore.Web.Models.Catalogue
{
    public class EditCatalogueViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DiscountType DiscountType { get; set; }

        public decimal DiscountAmount { get; set; }

        public IEnumerable<string> Promotions { get; set; }
    }
}