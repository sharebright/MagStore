using System;
using System.Collections.Generic;
using System.Linq;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Cart : IRavenEntity
    {
        public Cart()
        {
            OrderLines = new List<OrderLine>();
            Promotions = new List<Promotion>();
        }

        public string Id { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
        public IList<Promotion> Promotions { get; set; }

        public decimal Total
        {
            get 
            {
                return OrderLines.Sum(x => x.LinePrice);
            }
        }
    }
}