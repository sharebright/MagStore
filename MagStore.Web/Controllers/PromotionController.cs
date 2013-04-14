using System.Web.Mvc;
using MagStore.Web.Models.Promotion;

namespace MagStore.Web.Controllers
{
    public class PromotionController : Controller
    {
        public ActionResult Promotion()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public ActionResult CreatePromotion()
        {
            return View(new CreatePromotionViewModel());
        }

        [HttpPost]
        public ActionResult CreatePromotion(CreatePromotionInputModel inputModel)
        {
            return View(new CreatePromotionViewModel());
        }

        public ActionResult ViewPromotions()
        {
            return View(new PromotionsViewModel());
        }

        public ActionResult EditPromotion()
        {
            throw new System.NotImplementedException();
        }
    }
}