using System;
using MagStore.Data.Interfaces;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class Fulfillment : IRavenEntity
    {
        public Guid FulfillmentId { get; set; }
        public Guid OrderId { get; set; }
        public FulfillmentStatus FulfillmentStatus { get; set; }
    }
}