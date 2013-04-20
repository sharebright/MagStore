using RavenDbMembership.Entities.Enums;
using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Entities
{
    public class Fulfillment : IRavenEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public FulfillmentStatus FulfillmentStatus { get; set; }
    }
}