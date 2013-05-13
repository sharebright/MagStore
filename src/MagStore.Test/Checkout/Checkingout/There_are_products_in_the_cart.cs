using System.Collections.Generic;
using FluentAssertions;
using MagStore.Entities;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test.Checkout.Checkingout
{
    public class There_are_valid_cart_has_been_sent_to_checkout : When_checking_out
    {
        protected override void Arrange()
        {
            base.Arrange();
            var products = new List<string>();
            var product = string.Empty;
            products.Add(product);
            InputModel.Products = products;
            InputModel.SubTotal = 1;
        }

        [Test]
        public void Should_resolve_to_the_Checkout_view()
        {
            const string expectedView = "Checkout";
            Result.ViewName.Should().Be(expectedView, "Expected to Checkout as the view.");
        }

        [Test]
        public void Should_request_product_data_from_the_repository()
        {
            Shop.Received().GetCoordinator<Product>();
            Coordinator.Received().Load(Arg.Is(InputModel.Products));
        }
    }
}