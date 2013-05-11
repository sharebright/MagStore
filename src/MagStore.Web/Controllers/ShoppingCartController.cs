using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.ShoppingCart;
using SagePayMvc;

namespace MagStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShop shop;
        private ITransactionRegistrar registrar;

        public ShoppingCartController(IShop shop, ITransactionRegistrar registrar)
        {
            this.shop = shop;
            this.registrar = registrar;
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

        [HttpPost]
        public ActionResult Checkout(CheckoutPostInputModel inputModel)
        {
            var context = ControllerContext.RequestContext;
            var vendorTxCode = DateTime.Now.Ticks.ToString();
            var name = "The Name";
            var basket = new ShoppingBasket(name)
            {
                new BasketItem(1, "This is the Description", 22m),
            };


            var billingAddress = new Address
                {
                    Address1 = "Address1",
                    Address2 = "Address2",
                    City = "City",
                    Country = "GB",
                    Firstnames = "First",
                    Surname = "Last",
                    PostCode = "PostCode",
                    
                    Phone = "0912837482"
                };
           // var deliveryAddress = new Address();
            var customerEmail = "mark.gray@magsolutionslimited.co.uk";

            var response = registrar.Send(
                context,
                vendorTxCode,
                basket,
                billingAddress,
                billingAddress,
                customerEmail
            );

            return View();
        }
    }

    public class CheckoutPostInputModel
    {
    }

    public class CheckoutViewModel
    {
        public IList<Product> Products { get; set; }
    }
}
