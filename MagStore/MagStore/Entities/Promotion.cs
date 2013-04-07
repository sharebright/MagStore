using System;
using MagStore.Data.Interfaces;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class Promotion : IRavenEntity
    {
        public Promotion( DiscountPromotionCriteria criteria )
        {
            Criteria = criteria;
            DiscountPromotionValidator = new DiscountPromotionValidator( 0, null, 0, null );
        }

        public Guid Id { get; set; }
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