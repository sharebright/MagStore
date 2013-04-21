using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Azure;
using MagStore.Web.Models.Product;
using RavenDbMembership.Entities;
using RavenDbMembership.Entities.Enums;
using RavenDbMembership.Infrastructure.Interfaces;
using System.Linq;

namespace MagStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShop shop;
        private readonly IStorageAccessor storage;
        private readonly ProductControllerHelper productControllerHelper;

        public ProductController(IShop shop, IStorageAccessor storage)
        {
            this.shop = shop;
            this.storage = storage;
            productControllerHelper = new ProductControllerHelper(this.shop, this.storage);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var catalogues =
                shop.GetCoordinator<Catalogue>()
                    .Project()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            var promotions =
                shop.GetCoordinator<Promotion>()
                    .Project()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            return View(new CreateProductViewModel(catalogues, promotions));
        }

        [HttpGet]
        public ActionResult CreatePhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePhoto(CreatePhotoInputModel model)
        {
            //var client=storage.GetBlobClient();

            //CloudBlockBlob blob = null;
            //CloudBlobContainer cloudBlobContainer = null;
            //cloudBlobContainer = client.GetContainerReference("resources");
            //blob = cloudBlobContainer.GetBlockBlobReference(file.FileName);
            //blob.UploadFromStream(file.InputStream);

            var resources = model.File.Select(file => storage.AddBlobToResource(file.FileName, file.InputStream).ToString());

            ViewBag.Photos = storage.Resources.ListBlobs();
            return View();
        }

        public ActionResult ListPhotos()
        {
            ViewBag.Photos = storage.GetBlobClient().GetContainerReference("resources").ListBlobs();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(CreateProductInputModel inputModel)
        {
            if (ModelState.IsValid)
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
                    Supplier = inputModel.Supplier,
                    Images =
                        inputModel.File == null
                            ? new List<string>()
                            : productControllerHelper.SaveImagesToRaven(productControllerHelper.SaveImagesToAzure(productControllerHelper.ParseImagesFromModel(inputModel)))
                };

                shop.GetCoordinator<Product>().Save(product);

                return RedirectToAction("CreateProduct", "Product");
            }

            var catalogues =
                shop.GetCoordinator<Catalogue>()
                    .Project()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            var promotions =
                shop.GetCoordinator<Promotion>()
                    .Project()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            return View(new CreateProductViewModel(catalogues, promotions));
        }

        public ActionResult ViewProducts()
        {
            var products = shop.GetCoordinator<Product>().Project();
            return View(new ViewProductViewModel { Products = products });
        }

        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            var product = shop.GetCoordinator<Product>().Load(id);
            var editProductViewModel = productControllerHelper.GetEditProductViewModel(product);
            return View(editProductViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(EditProductInputModel inputModel)
        {
            var product = productControllerHelper.MapProductModelChangesToEntity(inputModel, shop.GetCoordinator<Product>().Load(inputModel.Id));
            shop.GetCoordinator<Product>().Save(product);
            return View(productControllerHelper.GetEditProductViewModel(product));
        }

        [HttpGet]
        public ActionResult ShowProducts()
        {
            var categories = Enum.GetNames(typeof(ProductType)).ToList();
            return View(new ProductCategoriesViewModel { Categories = categories });
        }

        [HttpGet]
        public ActionResult ViewProductsByCategory(ViewProductsByCategoryInputModel inputModel)
        {
            var products =
                shop.GetCoordinator<Product>()
                    .Project()
                    .Where(p => p.ProductType == (ProductType)Enum.Parse(typeof(ProductType), inputModel.Category));
            return View(new ViewProductsByCategoryViewModel { Products = products });
        }
    }
}