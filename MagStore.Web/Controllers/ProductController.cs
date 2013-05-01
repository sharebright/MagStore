using System;
using System.Collections.Generic;
using System.Web;
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

        private readonly IStorageAccessor storageAccessor;

        private readonly ProductControllerHelper productControllerHelper;

        public ProductController(IShop shop, IStorageAccessor storageAccessor)
        {
            this.shop = shop;
            this.storageAccessor = storageAccessor;
            productControllerHelper = new ProductControllerHelper(this.shop, this.storageAccessor);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var catalogues =
                shop.GetCoordinator<Catalogue>()
                    .List()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            var promotions =
                shop.GetCoordinator<Promotion>()
                    .List()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            return View(new CreateProductViewModel(catalogues, promotions));
        }

        private void UpdateImages(IEnumerable<KeyValuePair<string, string>> images)
        {
            foreach (var image in images)
            {
                UpdateImage(image.Key, image.Value);
            }
        }


        private void UpdateImage(string id, string imageType)
        {
            //bool hasChanges = image != null;
            //            Uri uri = null;
            //            if (hasChanges)
            //            {
            //                Stream inputStream = image.InputStream;
            //                uri = storageAccessor.AddBlobToResource(id, inputStream);
            //            }

            var img = shop.GetCoordinator<ProductImage>().Load(id);
            img.ImageType = imageType;
            //            if (uri != null) img.ImageUrl = uri.ToString();
            shop.GetCoordinator<ProductImage>()
                .Save(img);
        }

        private IEnumerable<string> CreateImages(IEnumerable<KeyValuePair<string, HttpPostedFileBase>> images)
        {
            var result = new List<string>();
            foreach (var image in images)
            {
                var id = Guid.NewGuid().ToString();
                CreateImage(image.Value, image.Key, id);
                result.Add(id);
            }
            return result;
        }

        private void CreateImage(HttpPostedFileBase image, string imageType, string fileName)
        {
            var inputStream = image.InputStream;
            var uri = storageAccessor.AddBlobToResource(fileName, inputStream);

            var img = new ProductImage
            {
                Id = fileName,
                ImageType = imageType,
                ImageUrl = uri.ToString()
            };
            shop.GetCoordinator<ProductImage>().Save(img);
        }


        [HttpPost]
        public ActionResult CreateProduct(CreateProductInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                var product = new Product
                {
                    Id = id,
                    Code = inputModel.Code,
                    Name = inputModel.Name,
                    Description = inputModel.Description,
                    Specification = inputModel.Specification,
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
                        inputModel.UploadedImages == null
                            ? new List<string>()
                            : CreateImages(productControllerHelper.ParseImagesFromModel(inputModel))
                };

                shop.GetCoordinator<Product>().Save(product);

                return RedirectToAction("EditProduct", new { id });
            }

            var catalogues =
                shop.GetCoordinator<Catalogue>()
                    .List()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            var promotions =
                shop.GetCoordinator<Promotion>()
                    .List()
                    .Select(x => new KeyValuePair<string, string>(x.Id, x.Name));

            return View(new CreateProductViewModel(catalogues, promotions));
        }

        public ActionResult ViewProducts()
        {
            var products = shop.GetCoordinator<Product>().List();
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
            var product = productControllerHelper
                .MapProductModelChangesToEntity(inputModel, shop.GetCoordinator<Product>().Load(inputModel.Id));

            var existingImages = new List<KeyValuePair<string, string>>();
            for (var i = 0; i < inputModel.ExistingImages.Count(); i++)
            {
                var imageAndType = new KeyValuePair<string, string>
                (
                    inputModel.ExistingImages.Skip(i).Take(1).Single(),
                    inputModel.ExistingPhotoType.Skip(i).Take(1).Single()
                );
                existingImages.Add(imageAndType);
            }

            UpdateImages(existingImages);

            product.Images = product.Images.Union
                (
                    CreateImages(productControllerHelper.ParseImagesFromModel(inputModel))
                );
            shop.GetCoordinator<Product>().Save(product);
            return RedirectToAction("EditProduct", new { inputModel.Id }); // View(productControllerHelper.GetEditProductViewModel(product));
        }

        [HttpGet]
        public ActionResult ShowProducts(ViewProductsByCategoryInputModel inputModel)
        {
            var products = shop.GetCoordinator<Product>().List();

            var availableCategories = from a in Enum.GetValues(typeof(ProductType)).AsQueryable().OfType<ProductType>()
                                      from p in products
                                      where p.ProductType == a
                                      where p.Gender.ToUpper() == inputModel.Gender.ToUpper()
                                      select a;

            var filteredProducts = availableCategories.SelectMany(a => products.Where(p => p.ProductType == a)).AsEnumerable();

            var images = shop.GetCoordinator<ProductImage>().Load(availableCategories.SelectMany(a => products.Where(p => p.ProductType == a).SelectMany(p => p.Images)));


            IDictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add("Gender", inputModel.Gender);
            return View(new ProductCategoriesViewModel { Categories = availableCategories, Products = filteredProducts, Images = images, Filters = filters });
        }

        [HttpGet]
        public ActionResult ViewProductsByCategory(ViewProductsByCategoryInputModel inputModel)
        {
            IEnumerable<Product> products =
                shop.GetCoordinator<Product>()
                    .List()
                    .Where(p => p.ProductType == (ProductType)Enum.Parse(typeof(ProductType), inputModel.Category));

            IQueryable<ProductType> productTypes = Enum.GetValues(typeof(ProductType)).AsQueryable().OfType<ProductType>();
            IQueryable<ProductType> availableCategories = from a in productTypes
                                                          from p in products
                                                          where p.ProductType == a
                                                          where p.Gender.ToUpper() == inputModel.Gender.ToUpper()
                                                          select a;

            IEnumerable<ProductImage> images = shop.GetCoordinator<ProductImage>().Load(availableCategories.SelectMany(a => products.Where(p => p.ProductType == a).SelectMany(p => p.Images)));

            IDictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add("Category", inputModel.Category);
            filters.Add("Gender", inputModel.Gender);

            return View(new ViewProductsByCategoryViewModel { Products = products, ProductType = inputModel.Category, Images = images, Filters = filters });
        }

        public ActionResult ShowProduct(ShowProductInputModel inputModel)
        {
            Product product = shop.Include<Product>(p => p.Images).Load(inputModel.Id);
            IEnumerable<ProductImage> images = shop.GetCoordinator<ProductImage>().Load(product.Images);
            var filters = new Dictionary<string, string>
            {
                {"Category", inputModel.Category},
                {"Gender", inputModel.Gender}
            };

            return View(new ShowProductViewModel { Product = product, ProductImages = images, Filters = filters });
        }

        [HttpGet]
        public ActionResult CreatePhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePhoto(CreatePhotoInputModel model)
        {
            //var client=storageAccessor.GetBlobClient();

            //CloudBlockBlob blob = null;
            //CloudBlobContainer cloudBlobContainer = null;
            //cloudBlobContainer = client.GetContainerReference("resources");
            //blob = cloudBlobContainer.GetBlockBlobReference(file.FileName);
            //blob.UploadFromStream(file.InputStream);

            var resources = model.File.Select(file => storageAccessor.AddBlobToResource(file.FileName, file.InputStream).ToString());

            ViewBag.Photos = storageAccessor.Resources.ListBlobs();
            return View();
        }

        public ActionResult ListPhotos()
        {
            ViewBag.Photos = storageAccessor.GetBlobClient().GetContainerReference("resources").ListBlobs();
            return View();
        }

        public ActionResult DeleteProduct(DeleteProductInputModel inputModel)
        {
            var product = shop.GetCoordinator<Product>().Load(inputModel.Id);
            shop.GetCoordinator<Product>().Delete(product);
            return RedirectToAction("ViewProducts");
        }
    }

    public class DeleteProductInputModel
    {
        public string Id { get; set; }
    }

    public class ShowProductViewModel
    {
        public Product Product { get; set; }

        public Dictionary<string, string> Filters { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }
    }

    public class ShowProductInputModel
    {
        public string Id { get; set; }

        public string Category { get; set; }

        public string Gender { get; set; }
    }
}