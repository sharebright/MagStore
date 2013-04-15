using System.Collections.Generic;
using RavenDBMembership.Entities;
using RavenDBMembership.Infrastructure.Interfaces;

namespace MagStore.Web.ShopHelpers
{
    public class PromotionHelper : IPromotionHelper
    {
        private readonly IShop shop;
        private IEnumerable<Promotion> existingPromotions;

        public PromotionHelper(IShop shop)
        {
            this.shop = shop;
            existingPromotions = new List<Promotion>();
        }

        public IEnumerable<Promotion> ExistingPromotions
        {
            get
            {
                existingPromotions = shop.GetCoordinator<Promotion>().Project();
                return existingPromotions;
            }
        }
    }

    public interface IPromotionHelper
    {
        IEnumerable<Promotion> ExistingPromotions { get; }
    }
}