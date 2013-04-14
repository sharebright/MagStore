using System;
using RavenDBMembership.Entities;
using RavenDBMembership.Entities.Enums;

namespace MagStore.Web.Models.Promotion
{
    public class CreatePromotionViewModel
    {
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