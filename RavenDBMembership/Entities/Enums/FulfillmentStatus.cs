﻿namespace RavenDbMembership.Entities.Enums
{
    public enum FulfillmentStatus
    {
        Ordered,
        Verified,
        ReadyForCollection,
        Collected,
        InTransit,
        ArrivedLocally,
        OutForDelivery,
        Delivered,
    }
}