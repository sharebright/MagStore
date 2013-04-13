using System;
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

        public ActionResult ViewProductsInCatalogue(string catalogueId)
        {
            var catalogue = shop.GetCoordinator<Catalogue>().Load(catalogueId);
            return View(new ProductsViewModel { Catalogue = catalogue });
        }

        public ActionResult ViewCatalogues()
        {
            var catalogues = shop.GetCoordinator<Catalogue>().Project();
            return View(new CataloguesViewModel { Catalogues = catalogues });
        }
    }
}
