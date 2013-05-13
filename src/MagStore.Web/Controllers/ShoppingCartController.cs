using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Models.ShoppingCart;
using SagePayMvc;

namespace MagStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShop shop;
        private readonly ITransactionRegistrar registrar;

        public bool UserIsAuthenticated
        {
            get { return User.Identity.IsAuthenticated; }
        }

        private User GetCurrentUser()
        {
            return Session["CurrentUser"] as User;
        }

        public ShoppingCartController(IShop shop, ITransactionRegistrar registrar)
        {
            this.shop = shop;
            this.registrar = registrar;
        }

        [HttpGet]
        public ActionResult ShoppingCart()
        {
            var user = GetCurrentUser();
            var shoppingCartViewModel = new ShoppingCartGetViewModel
                {
                    Cart = user.ShoppingCart,
                    Products = user.ShoppingCart.Products
                };
            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(AddToCartPostInputModel inputModel)
        {
            var user = GetCurrentUser();
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
            var user = GetCurrentUser();
            var productsThatHaveChanged = user.ShoppingCart.Products.Where(p => p.Id == inputModel.Id).ToList();
            var changeAmount = productsThatHaveChanged.Count - inputModel.Quantity;
            if (changeAmount > 0)
            {
                for (var i = 0; i < changeAmount; i++)
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
            var user = GetCurrentUser();
            var unchangedProduct = user.ShoppingCart
                                       .Products
                                       .Where(p => p.Id != inputModel.Id)
                                       .ToList();
            var productsToSave = new List<Product>(); //unchangedProduct.Union(productsThatHaveChanged).ToList();
            productsToSave.AddRange(unchangedProduct);
            user.ShoppingCart.Products = productsToSave;
            return RedirectToAction("ShoppingCart");
        }

        [HttpGet]
        public ActionResult Checkout(CheckoutGetInputModel inputModel)
        {
            if (UserIsAuthenticated)
            {
                var products = GetCurrentUser().ShoppingCart.Products;
                return View(new CheckoutViewModel {Products = products});
            }
            return RedirectToAction("LogOn", "Account");
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutPostInputModel inputModel)
        {
            /*
             * Get all the products in the basket : DONE
             * TODO: Apply intrinsic discounts
             * Display product summary : DONE
             * TODO: Offer to receive promo codes
             */
            if (!inputModel.Products.Any() || inputModel.SubTotal == 0)
            {
                return View("Error");
            }

            // TODO: Uncomment after tests - need to use a different technique for checking authentication
            //            if (!UserIsAuthenticated)
            //            {
            //                return RedirectToAction("LogOn", "Account");
            //            }

            var products = shop.GetCoordinator<Product>().Load(inputModel.Products).ToList();
            var subTotal = inputModel.SubTotal;
            var deliveryCharge = inputModel.DeliveryCharge;

            return View("Checkout", new CheckoutViewModel
            {
                Products = products,
                SubTotal = subTotal,
                DeliveryCharge = deliveryCharge
            });
        }


        [HttpPost]
        public ActionResult InitiatePayment(InitiatePaymentPostInputModel inputModel)
        {
            ValidateInputModel(inputModel);

            return View(new InitiatePaymentViewModel());
            //
            //            var context = ControllerContext.RequestContext;
            //            var vendorTxCode = DateTime.Now.Ticks.ToString();
            //            var name = "The Name";
            //            var basket = new ShoppingBasket(name)
            //            {
            //                new BasketItem(1, "This is the Description", 22m),
            //            };
            //
            //
            //            var billingAddress = new Address
            //                {
            //                    Address1 = "Address1",
            //                    Address2 = "Address2",
            //                    City = "City",
            //                    Country = "GB",
            //                    Firstnames = "First",
            //                    Surname = "Last",
            //                    PostCode = "PostCode",
            //                    
            //                    Phone = "0912837482"
            //                };
            //           // var deliveryAddress = new Address();
            //            var customerEmail = "mark.gray@magsolutionslimited.co.uk";
            //
            //            var response = registrar.Send(
            //                context,
            //                vendorTxCode,
            //                basket,
            //                billingAddress,
            //                billingAddress,
            //                customerEmail
            //            );
            //
        }

        private void ValidateInputModel(InitiatePaymentPostInputModel inputModel)
        {
            if (!(inputModel.Total > 0))
            {
                throw new InvalidOperationException("The total for this payment initiation must have a positive value.");
            }
            if (!inputModel.Products.Any())
            {
                throw new InvalidOperationException("There are no products to purchase attached to this payment initiation.");
            }
        }

        [HttpPost]
        public ActionResult SaveAddresses(SaveAddressesPostInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}