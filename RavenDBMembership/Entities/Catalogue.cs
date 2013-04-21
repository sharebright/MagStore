using System.Collections.Generic;
using RavenDbMembership.Entities.Enums;
using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Entities
{
    public class Catalogue : IRavenEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> Promotions { get; set; }
    }
}