using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using RavenDbMembership.Entities.Enums;

namespace MagStore.Web.Models.Product
{
    public class CreateProductInputModel : IProductPostInputModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        
        [Required(ErrorMessage = "A name must be supplied.")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Specification { get; set; }
        public string Catalogue { get; set; }
        public IEnumerable<string> Colours { get; set; }
        public IEnumerable<string> Sizes { get; set; }

        [Required(ErrorMessage="Please supply a value.")]
        [StringLength(50, MinimumLength = 4)]
        public string Gender { get; set; }

        public string Brand { get; set; }
        public string Supplier { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> Reviews { get; set; }
        public string[] PhotoType { get; set; }
        public HttpPostedFileBase[] UploadedImages { get; set; }

        [Required(ErrorMessage="The product must have a price.")]
        [Range(0.01, 10000.00, ErrorMessage = "The price must be at least 0.01")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify the product type.")]
        public ProductType ProductType { get; set; }

        public int[] AgeRange { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public IEnumerable<string> Promotions { get; set; }
    }

    public interface IProductPostInputModel
    {
        HttpPostedFileBase[] UploadedImages { get; set; }
        string[] PhotoType { get; set; }
    }
}