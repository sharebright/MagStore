using System.Collections.Generic;
using System.Linq;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class OrderLine
    {
        public IEnumerable<Product> Products { get; set; }

        private decimal SubTotal { get { return Products.Sum( x => x.Price ); } }

        private decimal DiscountAmount
        {
            get
            {
                decimal sum;
                var p = Products.First();
                if ( p == null ) return 0;

                if ( p.DiscountType == DiscountType.MonetaryAmount )
                {
                    sum = Products.Sum( x => x.DiscountAmount );
                }
                else
                {
                    sum = SubTotal / 100 * p.DiscountAmount;
                }
                return sum;
            }
        }

        public decimal LinePrice
        {
            get { return SubTotal - DiscountAmount; }
        }
    }
}