using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MagStore.Entities;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test.Checkout.InitiatingPayment
{
    public class  When_processing_a_payment_with_a_zero_total_amount : When_initiating_a_payment
    {
        private TestDelegate testDelegate;

        protected override void Arrange()
        {
            base.Arrange();
            var products = Substitute.For<IList<string>>();
            products.Add(string.Empty);
            InputModel.Products = products;
        }

        protected override void Act()
        {
            testDelegate = () => ControllerUnderTest.InitiatePayment(InputModel);
        }

        [Test]
        public void Should_throw_invalidoperationexception_if_the_initiatepaymentmodel_contains_a_zero_amount_total()
        {
            var message =
                Assert.Throws<InvalidOperationException>(
                    testDelegate,
                    "There is a positive total attached to this payment initiation.").Message;

            message.Should().Be("The total for this payment initiation must have a positive value.");
        }
    }
}