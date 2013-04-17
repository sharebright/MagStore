using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Web.Models.Product;
using RavenDBMembership.Entities;
using RavenDBMembership.Infrastructure.Interfaces;
using System.Linq;

namespace MagStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShop shop;

        public ProductController(IShop shop)
        {
            this.shop = shop;
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var catalogues =
                shop.GetCoordinator<Catalogue>()
                    .Project()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.CatalogueName));
            return View(new CreateProductViewModel(catalogues));
        }

        [HttpPost]
        public ActionResult CreateProduct(CreateProductInputModel inputModel)
        {
            var product = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = inputModel.Name,
                    Description = inputModel.Description,
                    Catalogue = inputModel.Catalogue,
                    Brand = inputModel.Brand,
                    Colour = inputModel.Colour,
                    DiscountAmount = inputModel.DiscountAmount,
                    DiscountType = inputModel.DiscountType,
                    Gender = inputModel.Gender,
                    Price = inputModel.Price,
                    ProductType = inputModel.ProductType,
                    Rating = inputModel.Rating,
                    Reviews = inputModel.Reviews,
                    Size = inputModel.Size,
                    Supplier = inputModel.Supplier
                };
            shop.GetCoordinator<Product>().Save(product);

            return RedirectToAction("CreateProduct", "Product");
        }

        public ActionResult ViewProducts()
        {
            var products = shop.GetCoordinator<Product>().Project();
            return View(new ViewProductViewModel { Products = products });
        }

        public ActionResult EditProduct(string id)
        {
            var p = shop.GetCoordinator<Product>().Load(id);
            return View(new EditProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Catalogue = p.Catalogue,
                Brand = p.Brand,
                Colour = p.Colour,
                DiscountAmount = p.DiscountAmount,
                DiscountType = p.DiscountType,
                Gender = p.Gender,
                Price = p.Price,
                ProductType = p.ProductType,
                Rating = p.Rating,
                Reviews = p.Reviews,
                Size = p.Size,
                Supplier = p.Supplier
            });
        }
    }
}