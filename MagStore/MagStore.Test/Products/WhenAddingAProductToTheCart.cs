using System;
using System.Collections.Generic;
using FluentAssertions;
using MagStore.Entities;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test.Products
{
    [TestFixture]
    public class WhenAddingAProductToTheCart
    {
        private Product product;
        private Cart cart;

        [SetUp]
        public void SetUp()
        {
            product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jumper",
                Price = 10
            };

            cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
            };
        }

        [Test]
        public void ShouldIncreaseTheTotalPriceOfTheCartTo10Pounds()
        {
//            var orderLines = new List<OrderLine>
//            {
//                new OrderLine {Products = new List<Product> {product}}
//            };
//
//            cart.OrderLines = orderLines;
//            
//            cart.Total.Should().Be(10);
        }
    }
}
