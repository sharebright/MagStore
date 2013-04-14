using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Web.Models.Catalogue;
using MagStore.Web.Models.Product;
using RavenDBMembership.Entities;
using RavenDBMembership.Infrastructure.Interfaces;

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
        public ActionResult ViewProductsInCatalogue(string Id)
        {
            var catalogue = shop.GetCoordinator<Catalogue>().Load(Id);
            return View(new ProductsViewModel { Catalogue = catalogue });
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
                Products = new List<Product>(),
                Promotions = new List<Promotion>()
            });
            return View(new CreateCatalogueViewModel());
        }
    }
}
