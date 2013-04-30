using System.Collections.Generic;
using MagStore.Web.Controllers;
using RavenDbMembership.Entities.Enums;

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