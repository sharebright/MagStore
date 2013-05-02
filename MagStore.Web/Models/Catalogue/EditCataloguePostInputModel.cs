using System.Collections.Generic;
using MagStore.Entities.Enums;
using MagStore.Web.Controllers;

namespace MagStore.Web.Models.Catalogue
{
    public class EditCataloguePostInputModel : EditCatalogueGetInputModel
    {
        public string Name { get; set; }

        public decimal DiscountAmount { get; set; }

        public DiscountType DiscountType { get; set; }

        public IEnumerable<string> Promotions { get; set; }
    }
}