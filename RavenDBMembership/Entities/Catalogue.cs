using System;
using System.Collections.Generic;
using RavenDBMembership.Entities.Enums;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Catalogue : IRavenEntity
    {
        public string Id { get; set; }
        public string CatalogueName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<Promotion> Promotions { get; set; } 
    }
}