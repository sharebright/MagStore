using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagStore.Azure;
using MagStore.Web.Models.Product;
using RavenDbMembership.Entities;
using RavenDbMembership.Infrastructure.Interfaces;

namespace MagStore.Web.Controllers
{
    using UploadedImages = IEnumerable<KeyValuePair<string, HttpPostedFileBase>>;
    using ExistingAzureImages = IEnumerable<KeyValuePair<string, string>>;
    
    public class ProductControllerHelper
    {
        private readonly IShop shop;
        private readonly IStorageAccessor storage;

        public ProductControllerHelper(IShop shop, IStorageAccessor storage)
        {
            this.shop = shop;
            this.storage = storage;
        }

        public IEnumerable<string> SaveImagesToRaven(IEnumerable<KeyValuePair<Guid, string>> savedInAzure)
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

        public IEnumerable<KeyValuePair<Guid, string>> SaveImagesToAzure(UploadedImages uploadedImageAndType)
        {
            uploadedImageAndType.Select(vp => storage.AddBlobToResource(vp.Value.FileName, vp.Value.InputStream));
            return uploadedImageAndType.Select(vp => new KeyValuePair<Guid, string>(Guid.NewGuid(), vp.Key)).ToList();
        }

        public IEnumerable<KeyValuePair<Guid, string>> UpdateImagesInAzure(UploadedImages uploadedImageAndType, ExistingAzureImages existingAzureImages)
        {
            return from e in existingAzureImages
                   from u in uploadedImageAndType
                   where e.Key == u.Key
                   select AddImageAndUpdate(e, u);
        }

        private KeyValuePair<Guid, string> AddImageAndUpdate(KeyValuePair<string, string> e, KeyValuePair<string, HttpPostedFileBase> u)
        {
            storage.AddBlobToResource(e.Value, u.Value.InputStream);
            return new KeyValuePair<Guid, string>(Guid.Parse(e.Key), u.Key);
        }

        public UploadedImages ParseImagesFromModel(CreateProductInputModel inputModel)
        {
            return inputModel.UploadedImages.Select((t, i) => new KeyValuePair<string, HttpPostedFileBase>(inputModel.PhotoType[i], t)).ToList();
        }

        public UploadedImages ParseImagesFromModel<T>(T inputModel) where T : IProductPostInputModel
        {
            if (inputModel.UploadedImages == null)
            {
                return new List<KeyValuePair<string, HttpPostedFileBase>>();
            }

            return inputModel.UploadedImages
                .Select((t, i) => new KeyValuePair<string, HttpPostedFileBase>(inputModel.PhotoType[i], t))
                .ToList();
        }

        public Product MapProductModelChangesToEntity(EditProductInputModel inputModel, Product product)
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

        private IEnumerable<ProductImage> UpdateImageChanges(IEnumerable<string> images, IEnumerable<HttpPostedFileBase> uploadedImages)
        {
            foreach (var productImage in images)
            {
                
            }

            return null;
        }

        public EditProductViewModel GetEditProductViewModel(Product p)
        {
            var catalogues = shop.GetCoordinator<Catalogue>().List();
            var images = shop.GetCoordinator<ProductImage>()
                            .Load(p.Images);
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
                    CatalogueList = catalogues,
                    Images = images
                };
            return editProductViewModel;
        }
    }
}