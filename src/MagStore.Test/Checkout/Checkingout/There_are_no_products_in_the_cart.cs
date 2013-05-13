using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Checkout.Checkingout
{
    public class There_are_no_products_in_the_cart : When_checking_out
    {
        protected override void Arrange()
        {
            base.Arrange();
            var products = new List<string>();
            var product = string.Empty;
            products.Add(product);
            InputModel.Products = new List<string>();
        }

        [Test]
        public void Should_resolve_to_the_Error_view_if_there_are_no_products_in_the_model()
        {
            const string expectedView = "Error";
            Result.ViewName.Should().Be(expectedView, "Expected no products on the model.");
        }
    }
}