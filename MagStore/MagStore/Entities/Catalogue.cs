using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class Catalogue : IRavenEntity
    {
        public Guid CatalogueId { get; set; }
        public string CatalogueName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<Promotion> Promotions { get; set; } 
    }
}