using System.Collections.Generic;
using System.Linq;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class DiscountPromotionValidator
    {
        public DiscountPromotionValidator()
            : this( 0, null, 0, null )
        {

        }
        public DiscountPromotionValidator( int priceMarker, IEnumerable<ProductType> productMarker, int quantityMarker, IEnumerable<Catalogue> catalogueMarker )
        {
            PriceMarker = priceMarker;
            ProductMarker = productMarker;
            QuantityMarker = quantityMarker;
            CatalogueMarker = catalogueMarker;
        }
        internal decimal PriceMarker { get; set; }
        internal IEnumerable<ProductType> ProductMarker { get; set; }
        internal int QuantityMarker { get; set; }
        internal IEnumerable<Catalogue> CatalogueMarker { get; set; }

        public IEnumerable<Promotion> ValidateAndReturnValidPromotions( Cart cart )
        {
            return new DiscountPromotionCriteria().ValidateAndReturnValidPromotions( this, cart );
        }

        internal bool CheckProductBasedDiscountCanBeApplied( IEnumerable<Product> products )
        {
            return ProductMarker.Any( marker => products.Any( product => product.ProductType == marker ) );
        }

        internal bool CheckPriceBasedDiscountCanBeApplied( decimal subTotal )
        {
            return subTotal >= PriceMarker;
        }

        internal bool CheckOrderQuantityBasedDiscountCanbeApplied( int productsInCart )
        {
            return productsInCart >= QuantityMarker;
        }

        internal bool CheckCatalogueBasedDiscountCanbeApplied( Cart cart )
        {
            return cart.OrderLines
                       .Select( orderLine => orderLine.Products )
                       .SelectMany( products => products )
                       .Any
                (
                    cartProduct => CatalogueMarker
                                       .Select( catalogue => catalogue.Products )
                                       .SelectMany( products => products.Select( g => g ) )
                                       .Any( catalogueProduct => cartProduct.Id == catalogueProduct.Id )
                );
        }
    }
}