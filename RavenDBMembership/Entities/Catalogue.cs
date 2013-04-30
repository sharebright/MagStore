using System.Collections.Generic;
using RavenDbMembership.Entities.Enums;
using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Entities
{
    public class Catalogue : IRavenEntity
    {
        private IEnumerable<string> promotions;

        public Catalogue()
        {
            Promotions = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> Promotions
        {
            get { return promotions ?? new List<string>(); }
            set { promotions = value; }
        }
    }
}