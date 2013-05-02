using System.Collections.Generic;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class Order : IRavenEntity
    {
        public string Id { get; set; }
        public string PaymentId { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<string> Products { get; set; }
    }
}