using System.Collections.Generic;

namespace MagStore.Web.Models.Promotion
{
    public class PromotionsViewModel
    {
        public IEnumerable<RavenDBMembership.Entities.Promotion> Promotions { get; set; }

        public PromotionsViewModel()
        {
            Promotions = new List<RavenDBMembership.Entities.Promotion>();
        }
    }
}