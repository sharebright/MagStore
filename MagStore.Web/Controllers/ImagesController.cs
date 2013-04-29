using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagStore.Azure;
using RavenDbMembership.Entities;
using RavenDbMembership.Entities.Enums;
using RavenDbMembership.Infrastructure.Interfaces;

namespace MagStore.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IShop shop;
        private readonly IStorageAccessor storageAccessor;

        public ImagesController(IShop shop, IStorageAccessor storageAccessor)
        {
            this.shop = shop;
            this.storageAccessor = storageAccessor;
        }

        [HttpGet]
        public ActionResult CreateImage()
        {
            return View(new ImageCreationGetModel());
        }

        [HttpGet]
        public ActionResult EditImage(ImageEditGetInputModel getInputModel)
        {
            var image = shop.GetCoordinator<ProductImage>()
                            .Load(getInputModel.Id);

            return View(new EditImageViewModel { Image = image });
        }

        [HttpPost]
        public ActionResult CreateImage(ImageCreationPostModel postModel)
        {
            //ValidateModel(postModel);
            var fileName = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                CreateImage(postModel.Image, postModel.ImageType.ToString(), fileName);
            }
            return RedirectToAction("EditImage", new { Id = fileName }); //View(new ImageCreationGetModel());
        }

        [HttpPost]
        public ActionResult EditImage(ImageEditPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = postModel.Id;
                UpdateImage(postModel, fileName);
            }

            return RedirectToAction( "EditImage", new { Id = postModel.Id }); //View(new ImageEditGetInputModel());
        }

        [HttpGet]
        public ActionResult DeleteImageFromProduct(string productId, string imageId)
        {
            var product = shop.GetCoordinator<Product>().Load(productId);
            product.Images = product.Images.Except(new[] {imageId});
            shop.GetCoordinator<Product>().Save(product);
            
            return RedirectToAction("EditProduct", "Product", new { Id = productId });
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

        private void UpdateImage(ImageEditPostModel postModel, string fileName)
        {
            bool hasChanges = postModel.Image != null;
            Uri uri = null;
            if (hasChanges)
            {
                Stream inputStream = postModel.Image.InputStream;
                uri = storageAccessor.AddBlobToResource(fileName, inputStream);
            }

            var image = shop.GetCoordinator<ProductImage>().Load(postModel.Id);
            image.ImageType = postModel.ImageType.ToString();
            if (uri != null) image.ImageUrl = uri.ToString();
            shop.GetCoordinator<ProductImage>()
                .Save(image);
        }
    }

    public class ImageEditGetInputModel
    {
        public string Id { get; set; }
    }

    public class EditImageViewModel
    {
        public ProductImage Image { get; set; }
        public IEnumerable<string> ImageTypes { get { return new[] { "" }.Union(Enum.GetNames(typeof(ImageType))); } }
    }

    public class ImageEditPostModel : ImageCreationPostModel
    {
        public string Id { get; set; }
    }

    public class ImageCreationPostModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a image type.")]
        public ImageType ImageType { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }

    public class ImageCreationGetModel
    {
        public IEnumerable<string> ImageTypes { get { return new[] { "" }.Union(Enum.GetNames(typeof(ImageType))); } }
    }
}
