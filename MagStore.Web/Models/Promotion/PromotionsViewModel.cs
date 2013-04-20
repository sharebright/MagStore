using System.Collections.Generic;

namespace MagStore.Web.Models.Promotion
{
    public class PromotionsViewModel
    {
        public IEnumerable<RavenDbMembership.Entities.Promotion> Promotions { get; set; }

        public PromotionsViewModel()
        {
            Promotions = new List<RavenDbMembership.Entities.Promotion>();
        }
    }
}