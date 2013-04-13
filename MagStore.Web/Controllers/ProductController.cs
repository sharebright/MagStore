using System;
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
            return View(new CreateProductInputModel());
        }

        [HttpPost]
        public ActionResult CreateProduct(CreateProductInputModel inputModel)
        {
            var product = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = inputModel.Name,
                    Description = inputModel.Description,
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

            return View(new CreateProductInputModel());
        }

        public ActionResult ViewProducts()
        {
            var products = shop.GetCoordinator<Catalogue>().Project().SelectMany(x => x.Products);
            return View(new ViewProductViewModel { Products = products });
        }

        public ActionResult EditProduct(string s)
        {
            throw new System.NotImplementedException();
        }
    }
}