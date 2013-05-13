using System.Collections.Generic;
using System.Web.Mvc;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using MagStore.Web.Controllers;
using MagStore.Web.Models.ShoppingCart;
using NSubstitute;
using SagePayMvc;

namespace MagStore.Test.Checkout.Checkingout
{
    public class When_checking_out : TestSetUpFixture
    {
        protected CheckoutPostInputModel InputModel;
        protected IShop Shop;
        protected ICoordinator<Product> Coordinator { get; set; }
        protected ShoppingCartController ControllerUnderTest { get; set; }
        protected ViewResult Result { get; set; }

        protected override void Arrange()
        {
            var product = new Product();

            Coordinator = Substitute.For<ICoordinator<Product>>();

            Coordinator.Load(Arg.Any<string>()).Returns(product);
            Coordinator.Load(Arg.Any<IEnumerable<string>>()).Returns(new[] {product});

            Shop = Substitute.For<IShop>();
            Shop.GetCoordinator<Product>().Returns(Coordinator);
            
            InputModel = Substitute.For<CheckoutPostInputModel>();
            var registrar = Substitute.For<ITransactionRegistrar>();
            ControllerUnderTest = new ShoppingCartController(Shop, registrar);
        }

        protected override void Act()
        {
            Result = ControllerUnderTest.Checkout(InputModel) as ViewResult;
        }
    }
}