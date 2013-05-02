using System.Collections.Generic;

namespace MagStore.Web.Models.Product
{
    public class ViewProductViewModel
    {
        public IEnumerable<Entities.Product> Products { get; set; }
    }
}