using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Entities;

namespace MagStore.Web.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly Store store;

        public CatalogueController(Store store)
        {
            this.store = store;
            store.GetCoordinator<Product>().Save(new Product {Id = Guid.NewGuid(), Name = "Test"});
        }

        public ActionResult All()
        {
            var products = store.GetCoordinator<Product>().List();
            return View(new CatalogueViewModel { Products = products });
        }
    }

    public class CatalogueViewModel
    {
        public IList<Product> Products { get; set; }
    }
}
