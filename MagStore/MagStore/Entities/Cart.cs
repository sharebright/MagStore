using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;
using System.Linq;

namespace MagStore.Entities
{
    public class Cart : IRavenEntity
    {
        public Guid CartId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }

        public IEnumerable<Promotion> Promotions { get; set; }

        public decimal Total
        {
            get 
            {
                return OrderLines.Sum(x => x.LinePrice);
            }
        }
    }
}