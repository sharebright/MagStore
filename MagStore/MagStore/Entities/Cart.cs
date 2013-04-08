using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;
using System.Linq;

namespace MagStore.Entities
{
    public class Cart : IRavenEntity
    {
        public Cart()
        {
            OrderLines = new List<OrderLine>();
            Promotions = new List<Promotion>();
        }

        public Guid CartId { get; set; }
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