using RavenDBMembership.Entities.Enums;

namespace RavenDBMembership.Entities
{
    public class ProductImage
    {
        public ImageType ImageType { get; set; }
        public string ImageUrl { get; set; }
    }
}