using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Checkout.Checkingout
{
    public class There_is_a_zero_sub_total_cart : When_checking_out
    {
        protected override void Arrange()
        {
            base.Arrange();
            var products = new List<string>();
            var product = string.Empty;
            products.Add(product);
            InputModel.Products = products;
            InputModel.SubTotal = 0;
        }

        [Test]
        public void Should_resolve_to_the_Error_view_if_there_is_a_zero_sub_total_in_the_cart()
        {
            const string expectedView = "Error";
            Result.ViewName.Should().Be(expectedView, "Expected zero sub total on the model.");
        }
    }
}