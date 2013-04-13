using System;
using RavenDBMembership.Entities.Enums;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Promotion : IRavenEntity
    {
        public Promotion( DiscountPromotionCriteria criteria )
        {
            Criteria = criteria;
            DiscountPromotionValidator = new DiscountPromotionValidator( 0, null, 0, null );
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountPromotionValidator DiscountPromotionValidator { get; set; }
        public DiscountPromotionCriteria Criteria { get; private set; }
    }
}