using System;
using System.Collections.Generic;
using RavenDBMembership.Entities.Enums;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Promotion : IRavenEntity
    {
        public Promotion()
        {
            Restrictions = new List<Promotion>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Exclusivity { get; set; }
        public IEnumerable<Promotion> Restrictions { get; set; }
    }
}