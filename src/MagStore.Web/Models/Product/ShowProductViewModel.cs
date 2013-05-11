using System.Collections.Generic;
using MagStore.Entities;

namespace MagStore.Web.Models.Product
{
    public class ShowProductViewModel
    {
        public Entities.Product Product { get; set; }

        public IEnumerable<KeyValuePair<string, Entities.Product>> ProductVariants { get; set; }

        public Dictionary<string, string> Filters { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }

        public IEnumerable<string> AvailableColours { get; set; }

        public IEnumerable<string> AvailableSizes { get; set; }
    }
}