using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;

namespace MagStore.Entities
{
    public class Order : IRavenEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}