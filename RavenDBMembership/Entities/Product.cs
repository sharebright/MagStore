using System.Collections.Generic;
using RavenDbMembership.Infrastructure.Interfaces;
using RavenDbMembership.Entities.Enums;

namespace RavenDbMembership.Entities
{
    public class Product : IRavenEntity
    {
        private IEnumerable<string> reviews;
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Brand { get; set; }
        public string Catalogue { get; set; }
        public string Supplier { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> Reviews
        {
            get { return reviews ?? new List<string>(); }
            set { reviews = value; }
        }

        public IEnumerable<string> Images { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int[] AgeRange { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> Promotions { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}