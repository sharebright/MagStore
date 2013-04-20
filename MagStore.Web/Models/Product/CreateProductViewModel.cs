using System.Collections.Generic;
using RavenDbMembership.Entities.Enums;

namespace MagStore.Web.Models.Product
{
    public class CreateProductViewModel
    {
        public CreateProductViewModel(IEnumerable<KeyValuePair<string, string>> catalogue, IEnumerable<KeyValuePair<string, string>> promotions)
        {
            Catalogue = catalogue;
            Promotions = promotions;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Catalogue { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> Reviews { get; set; }
        public IEnumerable<string> Images { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int[] AgeRange { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Promotions { get; set; }
    }
}