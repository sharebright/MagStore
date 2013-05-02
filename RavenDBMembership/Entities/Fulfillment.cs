using MagStore.Entities.Enums;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class Fulfillment : IRavenEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public FulfillmentStatus FulfillmentStatus { get; set; }
    }
}