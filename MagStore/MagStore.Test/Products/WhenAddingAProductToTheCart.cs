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
                Id = Guid.NewGuid(),
                Name = "Jumper",
                Price = 10
            };

            cart = new Cart
            {
                CartId = Guid.NewGuid(),
                OrderLines = new List<OrderLine>(),
                Promotions = new List<Promotion>()
            };
        }

        [Test]
        public void ShouldIncreaseTheTotalPriceOfTheCartTo10Pounds()
        {
            var orderLines = new List<OrderLine>
            {
                new OrderLine {Products = new List<Product> {product}}
            };

            cart.OrderLines = orderLines;
            
            cart.Total.Should().Be(10);
        }
    }
}
