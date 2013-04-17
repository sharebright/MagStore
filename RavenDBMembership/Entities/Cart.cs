using System;
using System.Collections.Generic;
using System.Linq;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Entities
{
    public class Cart : IRavenEntity
    {
        public Cart()
        {
            Products = new List<string>();
            Promotions = new List<string>();
        }

        public string Id { get; set; }
        public IList<string> Products { get; set; }
        public IList<string> Promotions { get; set; }
    }
}