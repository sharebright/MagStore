using System;
using System.Linq;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.Promotion;
using MagStore.Web.ShopHelpers;

namespace MagStore.Web.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IShop shop;
        private readonly PromotionHelper promotionHelper;

        public PromotionController(IShop shop)
        {
            this.shop = shop;
            promotionHelper = new PromotionHelper(shop);
        }

        public ActionResult Promotion()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public ActionResult CreatePromotion()
        {
            return View(new CreatePromotionViewModel(promotionHelper));
        }

        [HttpPost]
        public ActionResult CreatePromotion(CreatePromotionInputModel inputModel)
        {
            var promotion = new Promotion
            {
                Id = Guid.NewGuid().ToString(),
                Name = inputModel.Name,
                Code = inputModel.Code,
                ValidFrom = inputModel.ValidFrom,
                ValidTo = inputModel.ValidTo,
                DiscountAmount = inputModel.DiscountAmount,
                DiscountType = inputModel.DiscountType,
                Exclusivity = inputModel.Exclusivity,
                Restrictions = inputModel.Restrictions.ToList()
            };

            shop.GetCoordinator<Promotion>().Save(promotion);

            return RedirectToAction("EditPromotion", "Promotion", new {promotion.Id});
        }

        public ActionResult ViewPromotions()
        {
            var promotions = shop.GetCoordinator<Promotion>().List();
            return View(new PromotionsViewModel{Promotions = promotions});
        }

        public ActionResult EditPromotion(string id)
        {
            var p = shop.GetCoordinator<Promotion>().Load(id);
            return View(new PromotionViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                ValidFrom = p.ValidFrom,
                ValidTo = p.ValidTo,
                DiscountType = p.DiscountType,
                DiscountAmount = p.DiscountAmount,
                Exclusivity = p.Exclusivity,
                Restrictions = p.Restrictions
            });
        }

        public ActionResult ViewPromotion(string id)
        {
            var p = shop.GetCoordinator<Promotion>().Load(id);
            return View(new PromotionViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    ValidFrom = p.ValidFrom,
                    ValidTo = p.ValidTo,
                    DiscountType = p.DiscountType,
                    DiscountAmount = p.DiscountAmount,
                    Exclusivity = p.Exclusivity,
                    Restrictions = p.Restrictions
                });
        }
    }
}