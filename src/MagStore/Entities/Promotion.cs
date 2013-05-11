using System;
using System.Collections.Generic;
using MagStore.Entities.Enums;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class Promotion : IRavenEntity
    {
        public Promotion()
        {
            Restrictions = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Exclusivity { get; set; }
        public IEnumerable<string> Restrictions { get; set; }
    }
}