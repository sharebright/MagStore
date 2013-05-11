using System.Collections.Generic;

namespace MagStore.Web.Models.Promotion
{
    public class PromotionsViewModel
    {
        public IEnumerable<Entities.Promotion> Promotions { get; set; }

        public PromotionsViewModel()
        {
            Promotions = new List<Entities.Promotion>();
        }
    }
}