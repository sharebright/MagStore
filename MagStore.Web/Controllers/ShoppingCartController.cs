using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.ShoppingCart;

namespace MagStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShop shop;

        public ShoppingCartController(IShop shop)
        {
            this.shop = shop;
        }

        [HttpGet]
        public ActionResult ShoppingCart()
        {
            var user = Session["CurrentUser"] as User;
            var shoppingCartViewModel = new ShoppingCartGetViewModel
            {
                Cart = user.ShoppingCart,
                Products = user.ShoppingCart.Products// LoadProductsFromCart(user.ShoppingCart)
            };
            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(AddToCartPostInputModel inputModel)
        {
            var user = Session["CurrentUser"] as User;
                var product = shop.GetCoordinator<Product>()
                                      .Query<Product>()
                                      .Single(p => p.Code == inputModel.Code 
                                            && p.Colour == inputModel.Colour 
                                            && p.Size == inputModel.Size);
                user.ShoppingCart.Products.Add(product);

            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public ActionResult UpdateProductQuantity(UpdateProductQuantityPostInputModel inputModel)
        {
            var user = (Session["CurrentUser"] as User);
            var productsThatHaveChanged = user.ShoppingCart.Products.Where(p => p.Id == inputModel.Id).ToList();
            var changeAmount = productsThatHaveChanged.Count - inputModel.Quantity;
            if (changeAmount > 0)
            {
                for (var i = 0; i < changeAmount; i++ )
                    productsThatHaveChanged.Remove(productsThatHaveChanged.First(p => p.Id == inputModel.Id));
            }
            else if (changeAmount < 0)
            {
                var abs = Math.Abs(changeAmount);
                for (var i = 0; i < abs; i++)
                    productsThatHaveChanged.Add(productsThatHaveChanged.First(p => p.Id == inputModel.Id));
            }
            else
            {
                return RedirectToAction("ShoppingCart");
            }

            var unchangedProduct = user.ShoppingCart
                .Products
                .Where(p => p.Id != inputModel.Id)
                .ToList();

            var productsToSave = new List<Product>(); //unchangedProduct.Union(productsThatHaveChanged).ToList();
            productsToSave.AddRange(unchangedProduct);
            productsToSave.AddRange(productsThatHaveChanged);
            user.ShoppingCart.Products = productsToSave;

            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public ActionResult RemoveProductFromBasket(RemoveProductFromBasketPostInputModel inputModel)
        {
            var user = (Session["CurrentUser"] as User);
            var unchangedProduct = user.ShoppingCart
                .Products
                .Where(p => p.Id != inputModel.Id)
                .ToList();
            var productsToSave = new List<Product>(); //unchangedProduct.Union(productsThatHaveChanged).ToList();
            productsToSave.AddRange(unchangedProduct);
            user.ShoppingCart.Products = productsToSave;
            return RedirectToAction("ShoppingCart");
        }

        public ActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var products = (Session["CurrentUser"] as User).ShoppingCart.Products;
                return View(new CheckoutViewModel {Products = products});
            }
            return RedirectToAction("LogOn", "Account");
        }
    }

    public class CheckoutViewModel
    {
        public IList<Product> Products { get; set; }
    }
}
