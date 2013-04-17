using System;
using System.Collections.Generic;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Order : IRavenEntity
    {
        public string Id { get; set; }
        public string PaymentId { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}