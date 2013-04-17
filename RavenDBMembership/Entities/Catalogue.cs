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
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> Promotions { get; set; } 
    }
}