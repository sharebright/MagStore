using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagStore.Azure;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.Shop;

namespace MagStore.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShop shop;
        private IStorageAccessor storageAccessor;

        public ShopController(IShop shop, IStorageAccessor storageAccessor)
        {
            this.shop = shop;
            this.storageAccessor = storageAccessor;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateCloudbird()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public ActionResult UpdateAzure()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public ActionResult ChangeShopSettings()
        {
            var settings = shop.GetSettings();
            var logo = new ProductImage();
            if (!string.IsNullOrEmpty(settings.Logo))
            {
                logo = shop.GetCoordinator<ProductImage>().Load(settings.Logo);
            }
            return View(new ShopSettingsViewModel(settings, logo));
        }

        [HttpPost]
        public ActionResult ChangeShopSettings(ChangeShopSettingsPostInputModel inputModel)
        {
            IShopSettings settings = shop.GetSettings();
            settings.CurrencySymbol = inputModel.CurrencySymbol;
            settings.CurrencyFormat = inputModel.CurrencyFormat;
            settings.CurrencyConversion = inputModel.CurrencyConversion;
            settings.DeliveryCharge = inputModel.DeliveryCharge;
            settings.Name = inputModel.Name;
            settings.TagLine = inputModel.TagLine;

            settings.Logo = inputModel.Logo == null
                                ? string.Empty
                                : CreateImages(new[]
                                {
                                    new KeyValuePair<string, HttpPostedFileBase>("Thumb", inputModel.Logo)
                                })
                                .First();

            shop.UpdateSettings(settings);
            return RedirectToAction("ChangeShopSettings");
        }

        public ActionResult DeleteLogoFromSettings(DeleteLogoFromSettingsPostInputModel inputModel)
        {
            var settings = shop.GetSettings();

            settings.Logo = string.Empty;

            shop.UpdateSettings(settings);

            return RedirectToAction("ChangeShopSettings");
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