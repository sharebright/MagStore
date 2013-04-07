using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class Product : IRavenEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public Gender Gender { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public int Rating { get; set; }
        public Guid Reviews { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int[] AgeRange { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<Guid> Promotions { get; set; }
    }
}