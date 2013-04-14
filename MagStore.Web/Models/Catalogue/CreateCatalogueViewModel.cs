using System.Collections.Generic;
using RavenDBMembership.Entities;
using RavenDBMembership.Entities.Enums;

namespace MagStore.Web.Models.Catalogue
{
    public class CreateCatalogueViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RavenDBMembership.Entities.Product> Products { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<RavenDBMembership.Entities.Promotion> Promotions { get; set; } 
    }
}