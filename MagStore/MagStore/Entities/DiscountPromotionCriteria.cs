using System.Collections.Generic;
using System.Linq;

namespace MagStore.Entities
{
    public class DiscountPromotionCriteria
    {
        private bool checkProductBasedCriteria;
        private bool checkPriceBasedCriteria;
        private bool checkOrderQuantityBasedCriteria;
        private bool checkCatalogueBasedCriteria;
        private bool checkFreeForAllBasedCriteria;

        public DiscountPromotionCriteria(bool checkProductBasedCriteria, 
                                           bool checkPriceBasedCriteria, 
                                           bool checkOrderQuantityBasedCriteria, 
                                           bool checkCatalogueBasedCriteria, 
                                           bool checkFreeForAllBasedCriteria)
        {
            this.checkProductBasedCriteria = checkProductBasedCriteria;
            this.checkPriceBasedCriteria = checkPriceBasedCriteria;
            this.checkOrderQuantityBasedCriteria = checkOrderQuantityBasedCriteria;
            this.checkCatalogueBasedCriteria = checkCatalogueBasedCriteria;
            this.checkFreeForAllBasedCriteria = checkFreeForAllBasedCriteria;
        }

        public DiscountPromotionCriteria()
        {
        }

        public IEnumerable<Promotion> ValidateAndReturnValidPromotions(DiscountPromotionValidator discountPromotionValidator,Cart cart)
        {
            var promotions = cart.Promotions;
            var validPromotions = new List<Promotion>();
            var orderLines = cart.OrderLines.ToList();
            var orderSubTotal = orderLines.Sum(orderLine => orderLine.LinePrice);
            var products = orderLines.Select( x => x.Products ).SelectMany( x => x ).ToList();
            var productsInCart = products.Count();
            foreach (var promotion in promotions)
            {
                CalibrateValidationBasedOnThePromotion(discountPromotionValidator, promotion);

                if (checkProductBasedCriteria)
                {
                    if (discountPromotionValidator.CheckProductBasedDiscountCanBeApplied(products))
                    {
                        validPromotions.Add(promotion);
                        continue;
                    }
                }
                if (checkPriceBasedCriteria)
                {
                    if (discountPromotionValidator.CheckPriceBasedDiscountCanBeApplied(orderSubTotal))
                    {
                        validPromotions.Add(promotion);
                        continue;
                    }
                }
                if (checkOrderQuantityBasedCriteria)
                {
                    if (discountPromotionValidator.CheckOrderQuantityBasedDiscountCanbeApplied(productsInCart))
                    {
                        validPromotions.Add(promotion);
                        continue;
                    }
                }
                if (checkCatalogueBasedCriteria)
                {
                    if (discountPromotionValidator.CheckCatalogueBasedDiscountCanbeApplied(cart))
                    {
                        validPromotions.Add(promotion);
                        continue;
                    }
                }
                if (checkFreeForAllBasedCriteria)
                {
                    validPromotions.Add(promotion);
                }
            }
            return validPromotions;
        }

        private void CalibrateValidationBasedOnThePromotion(DiscountPromotionValidator discountPromotionValidator,
                                                                   Promotion promotion)
        {
//            checkProductBasedCriteria = promotion.Criteria.checkProductBasedCriteria;
//            checkPriceBasedCriteria = promotion.Criteria.checkPriceBasedCriteria;
//            checkOrderQuantityBasedCriteria = promotion.Criteria.checkOrderQuantityBasedCriteria;
//            checkCatalogueBasedCriteria = promotion.Criteria.checkCatalogueBasedCriteria;
//            checkFreeForAllBasedCriteria = promotion.Criteria.checkFreeForAllBasedCriteria;            
//            discountPromotionValidator.CatalogueMarker = promotion.DiscountPromotionValidator.CatalogueMarker;
//            discountPromotionValidator.PriceMarker = promotion.DiscountPromotionValidator.PriceMarker;
//            discountPromotionValidator.ProductMarker = promotion.DiscountPromotionValidator.ProductMarker;
//            discountPromotionValidator.QuantityMarker = promotion.DiscountPromotionValidator.QuantityMarker;
        }
    }
}