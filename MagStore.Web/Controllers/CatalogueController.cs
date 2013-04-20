using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MagStore.Web.Models.Catalogue;
using MagStore.Web.Models.Product;
using RavenDbMembership.Entities;
using RavenDbMembership.Infrastructure.Interfaces;

namespace MagStore.Web.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IShop shop;

        public CatalogueController(IShop shop)
        {
            this.shop = shop;
        }

        [HttpPost]
        public ActionResult ViewProductsInCatalogue(string id)
        {
            var products = shop.GetCoordinator<Product>().Project().Where(x=>x.Catalogue == id);
            var catalogue = shop.GetCoordinator<Catalogue>().Load(id);
            return View(new ProductsViewModel { Catalogue = catalogue, Products = products });
        }

        public ActionResult ViewCatalogues()
        {
            var catalogues = shop.GetCoordinator<Catalogue>().Project();
            return View(new CataloguesViewModel { Catalogues = catalogues });
        }

        [HttpGet]
        public ActionResult CreateCatalogue()
        {
            return View(new CreateCatalogueViewModel());
        }

        [HttpPost]
        public ActionResult CreateCatalogue(CreateCatalogueInputModel inputModel)
        {
            shop.GetCoordinator<Catalogue>().Save(new Catalogue
            {
                Id=inputModel.Id,
                CatalogueName=inputModel.Name,
                DiscountAmount =  inputModel.DiscountAmount,
                DiscountType = inputModel.DiscountType,
                Promotions = new List<string>()
            });
            return RedirectToAction("ViewCatalogues", "Catalogue"); // View(new CreateCatalogueViewModel());
        }
    }
}
