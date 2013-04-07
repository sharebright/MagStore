using System;
using System.Web.Mvc;
using MagStore;
using MagStore.Data.Interfaces;
using MagStore.Entities;
using MagStore.Entities.Enums;
using MagStore.Mvc;

namespace RdbTestMag.Mvc.Controllers
{
    [HandleError]
    public class HomeController : MagController
    {
        private readonly Store store;

        public HomeController(Store store)
        {
            this.store = store;
        }

        public ActionResult Index(Guid? id)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ShoppingCart()
        {
            var catalogue = new Catalogue {CatalogueId = Guid.NewGuid()};
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Peplum Dress",
                Price = 2,
                ProductType = ProductType.Dresses,
            };
            catalogue.Products = new[]
            {
                product1
            };
            var products = new[]
            {
                product1
            };
            var orderLine = new OrderLine { Products = products };

            var discountApplicationCriteria = new DiscountPromotionCriteria(false, true, false, false, false);

            var promotionValidator = new DiscountPromotionValidator(5, null, 0, null);
            var promotion = new Promotion(discountApplicationCriteria)
            {
                Code = "123",
                DiscountAmount = 5,
                DiscountType = DiscountType.Percentage,
                Id = Guid.NewGuid(),
                Name = "Test",
                ValidFrom = DateTime.Now.AddDays(-1),
                ValidTo = DateTime.Now.AddDays(1),
                DiscountPromotionValidator=promotionValidator
            };
            

            var c = new Cart
            {
                CartId = Guid.NewGuid(),
                OrderLines = new[]
                {
                    orderLine,
                },
                Promotions = new[]
                {
                    promotion,
                }
            };


            var validPromos = new DiscountPromotionValidator().ValidateAndReturnValidPromotions(c);
            
            return View();
        }
    }

    public class DataItem : IRavenEntity
    {
        public DataItem()
        {
            Id = Guid.NewGuid();
            Name = "Bob";
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CompoundItem : IRavenEntity
    {
        public Guid Id { get; set; }

        public DataItem Data { get; set; }
    }
}

