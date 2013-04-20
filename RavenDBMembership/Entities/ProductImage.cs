using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Entities
{
    public class ProductImage : IRavenEntity
    {
        public string Id { get; set; }
        public string ImageType { get; set; }
        public string ImageUrl { get; set; }
    }
}