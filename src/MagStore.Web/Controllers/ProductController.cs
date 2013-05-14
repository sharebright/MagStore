using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagStore.Azure;
using MagStore.Entities;
using MagStore.Entities.Enums;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.Product;

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

        [HttpPost]
        public ActionResult CreateProduct(CreateProductInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var sizes = inputModel.Sizes;
                var products = (from c in inputModel.Colours
                                where sizes != null
                                from s in sizes
                                select new Product
                 {
                     Id = Guid.NewGuid().ToString(),
                     Code = inputModel.Code,
                     Name = inputModel.Name,
                     Description = inputModel.Description,
                     Specification = inputModel.Specification,
                     Catalogue = inputModel.Catalogue,
                     Brand = inputModel.Brand,
                     Colour = c,
                     DiscountAmount = inputModel.DiscountAmount,
                     DiscountType = inputModel.DiscountType,
                     Gender = inputModel.Gender,
                     Price = inputModel.Price,
                     ProductType = inputModel.ProductType,
                     Rating = inputModel.Rating,
                     Reviews = inputModel.Reviews,
                     Size = s,
                     Supplier = inputModel.Supplier,
                     Tags = inputModel.Tags,
                     Images =
                         inputModel.UploadedImages == null
                             ? new List<string>()
                             : CreateImages(productControllerHelper.ParseImagesFromModel(inputModel))
                 }).ToList();

                shop.GetCoordinator<Product>().Save(products);

                return RedirectToAction("EditProduct", new { id = products.First().Id });
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
            return View(new ViewProductViewModel { Products = OrderedProducts() });
        }

        private IEnumerable<Product> OrderedProducts()
        {
            var enumerator = shop.GetCoordinator<Product>()
                                 .List()
                                 .OrderByDescending(p => p.Code)
                                 .ThenBy(p => p.Id)
                                 .GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            // TODO: Sort products before pumping them out
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
            if (inputModel.ExistingImages != null)
            {
                for (var i = 0; i < inputModel.ExistingImages.Count(); i++)
                {
                    var imageAndType = new KeyValuePair<string, string>
                        (
                        inputModel.ExistingImages.Skip(i).Take(1).Single(),
                        inputModel.ExistingPhotoType.Skip(i).Take(1).Single()
                        );
                    existingImages.Add(imageAndType);
                }
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

            var filteredProducts = availableCategories
                .SelectMany(a => products.Where(p => p.ProductType == a))
                .AsEnumerable();

            var images = shop
                .GetCoordinator<ProductImage>()
                .Load(availableCategories
                .SelectMany(a => products
                    .Where(p => p.ProductType == a)
                    .SelectMany(p => p.Images))
                )
                .Where(i => i.ImageType == ImageType.Thumb.ToString());


            IDictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add("Gender", inputModel.Gender);
            
            return View(new ProductCategoriesViewModel
            {
                Categories = availableCategories, 
                Products = filteredProducts,
                Images = images, 
                Filters = filters
            });
        }

        [HttpGet]
        public ActionResult ViewProductsByCategory(ViewProductsByCategoryInputModel inputModel)
        {
            var products =
                shop.GetCoordinator<Product>()
                    .List()
                    .Where(p => p.ProductType == (ProductType)Enum.Parse(typeof(ProductType), inputModel.Category));

            var productTypes = Enum.GetValues(typeof(ProductType)).AsQueryable().OfType<ProductType>();
            var availableCategories = from a in productTypes
                                      from p in products
                                      where p.ProductType == a
                                      where p.Gender.ToUpper() == inputModel.Gender.ToUpper()
                                      select a;

            var imagesCoordinator = shop.GetCoordinator<ProductImage>();
            var ids = availableCategories
                .SelectMany(a => products
                    .Where(p => p.ProductType == a)
                    .SelectMany(p => p.Images));

            var productImages = imagesCoordinator.Load(ids);

            var thumbs = productImages.Where(i => i.ImageType == ImageType.Thumb.ToString());

            IDictionary<string, string> filters = new Dictionary<string, string>();
            filters.Add("Category", inputModel.Category);
            filters.Add("Gender", inputModel.Gender);

            return View(new ViewProductsByCategoryViewModel { Products = products, ProductType = inputModel.Category, Images = thumbs, Filters = filters });
        }

        public ActionResult ShowProduct(ShowProductInputModel inputModel)
        {
            var productCoordinator = shop.GetCoordinator<Product>();
            var ravenQueryable = productCoordinator.List();
            var query = ravenQueryable.Where(p => p.Code == inputModel.Code);
            var productKeyValuePairs = query.Select(d => new KeyValuePair<string, Product>(d.Id, d));
            var products = productKeyValuePairs.ToList();

            var product = products.First().Value;//shop.Include<Product>(p => p.Images).Load(inputModel.Id);
            var images =
                shop.GetCoordinator<ProductImage>()
                    .Load(product.Images)
                    .Where(i => i.ImageType == ImageType.Feature.ToString());

            var availableColours = products.Select(c => c.Value.Colour);
            var availableSizes = products.Select(c => Convert.ToInt32(c.Value.Size)).OrderBy(c => c).Select(c => c.ToString());

            var filters = new Dictionary<string, string>
            {
                {"Category", inputModel.Category},
                {"Gender", inputModel.Gender},
                {"Code", inputModel.Code}
            };

            return View(new ShowProductViewModel { Product = product, ProductImages = images, ProductVariants = products, AvailableColours = availableColours, AvailableSizes = availableSizes, Filters = filters });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteProduct(DeleteProductInputModel inputModel)
        {
            var product = shop.GetCoordinator<Product>().Load(inputModel.Id);
            shop.GetCoordinator<Product>().Delete(product);
            return RedirectToAction("ViewProducts");
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
            var img = shop.GetCoordinator<ProductImage>().Load(id);
            img.ImageType = imageType;
            shop.GetCoordinator<ProductImage>().Save(img);
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
    }
}