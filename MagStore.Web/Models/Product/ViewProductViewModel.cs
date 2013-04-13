using System.Collections.Generic;

namespace MagStore.Web.Models.Product
{
    public class ViewProductViewModel
    {
        public IEnumerable<RavenDBMembership.Entities.Product> Products { get; set; }
    }
}