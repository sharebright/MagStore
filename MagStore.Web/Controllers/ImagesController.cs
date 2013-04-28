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

        [HttpPost]
        public ActionResult CreateImage(ImageCreationPostModel postModel)
        {
            //ValidateModel(postModel);
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString();
                Stream inputStream = postModel.Image.InputStream;
                Uri uri = storageAccessor.AddBlobToResource(fileName, inputStream);

                ProductImage image = new ProductImage
                    {
                        Id = fileName,
                        ImageType = postModel.ImageType.ToString(),
                        ImageUrl = uri.ToString()
                    };
                shop.GetCoordinator<ProductImage>().Save(image);
            }
            return View(new ImageCreationGetModel());
        }
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
