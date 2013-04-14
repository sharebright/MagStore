using System;
using RavenDBMembership.Entities.Enums;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Fulfillment : IRavenEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public FulfillmentStatus FulfillmentStatus { get; set; }
    }
}