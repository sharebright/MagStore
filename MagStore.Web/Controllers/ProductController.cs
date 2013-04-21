using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MagStore.Azure;
using MagStore.Web.Models.Product;
using RavenDbMembership.Entities;
using RavenDbMembership.Infrastructure.Interfaces;
using System.Linq;

namespace MagStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShop shop;
        private readonly IStorageAccessor storage;

        public ProductController(IShop shop, IStorageAccessor storage)
        {
            this.shop = shop;
            this.storage = storage;
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

            var resources = new List<string>();
            foreach (var file in model.File)
            {
                resources.Add(storage.AddBlobToResource(file.FileName, file.InputStream).ToString());
            }

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
                            : SaveImagesToRaven(SaveImagesToAzure(ParseImagesFromModel(inputModel)))
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

        private IEnumerable<string> SaveImagesToRaven(IEnumerable<KeyValuePair<Uri, string>> savedInAzure)
        {
            var images = new List<string>();
            foreach (var vp in savedInAzure)
            {
                var productImage = new ProductImage
                {
                    Id = vp.Key.ToString(), 
                    ImageType = vp.Value, 
                    ImageUrl = vp.Key.ToString()
                };
                shop.GetCoordinator<ProductImage>().Save(productImage);
                images.Add(vp.Key.ToString());
            }
            return images;
        }

        private IEnumerable<KeyValuePair<Uri, string>> SaveImagesToAzure(IEnumerable<KeyValuePair<string, HttpPostedFileBase>> uploadedImageAndType)
        {
            return uploadedImageAndType.Select(vp => new KeyValuePair<Uri, string>(storage.AddBlobToResource(vp.Value.FileName, vp.Value.InputStream), vp.Key)).ToList();
        }

        private IEnumerable<KeyValuePair<string, HttpPostedFileBase>> ParseImagesFromModel(CreateProductInputModel inputModel)
        {
            return inputModel.File.Select((t, i) => new KeyValuePair<string, HttpPostedFileBase>(inputModel.PhotoType[i], t)).ToList();
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
            var editProductViewModel = GetEditProductViewModel(product);
            return View(editProductViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(EditProductInputModel inputModel)
        {
            var product = MapProductModelChangesToEntity(inputModel, shop.GetCoordinator<Product>().Load(inputModel.Id));
            shop.GetCoordinator<Product>().Save(product);
            return View(GetEditProductViewModel(product));
        }

        private Product MapProductModelChangesToEntity(EditProductInputModel inputModel, Product product)
        {
            product.AgeRange = inputModel.AgeRange;
            product.Brand = inputModel.Brand;
            product.Catalogue = inputModel.Catalogue;
            product.Colour = inputModel.Colour;
            product.Description = inputModel.Description;
            product.DiscountAmount = inputModel.DiscountAmount;
            product.DiscountType = inputModel.DiscountType;
            product.Gender = inputModel.Gender;
            product.Name = inputModel.Name;
            product.Price = inputModel.Price;
            product.ProductType = inputModel.ProductType;
            product.Promotions = inputModel.Promotions;
            product.Rating = inputModel.Rating;
            product.Reviews = inputModel.Reviews;
            product.Size = inputModel.Size;
            product.Supplier = inputModel.Supplier;
            return product;
        }

        private EditProductViewModel GetEditProductViewModel(Product p)
        {
            var catalogues = shop.GetCoordinator<Catalogue>().Project();
            var editProductViewModel = new EditProductViewModel
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
                    Supplier = p.Supplier,
                    CatalogueList = catalogues
                };
            return editProductViewModel;
        }
    }

    public class CreatePhotoInputModel
    {
        public string[] PhotoType { get; set; }
        public HttpPostedFileBase[] File { get; set; }
    }
}