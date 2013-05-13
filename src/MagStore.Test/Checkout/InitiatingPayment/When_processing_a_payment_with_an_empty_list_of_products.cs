using System;
using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Checkout.InitiatingPayment
{
    public class When_processing_a_payment_with_an_empty_list_of_products : When_initiating_a_payment
    {
        private TestDelegate testDelegate;

        protected override void Arrange()
        {
            base.Arrange();
            InputModel.Total = 1;
        }

        protected override void Act()
        {
            testDelegate = () => ControllerUnderTest.InitiatePayment(InputModel);
        }

        [Test]
        public void Should_throw_invalidoperationexception_if_the_initiatepaymentmodel_contains_an_empty_list_of_products()
        {
            testDelegate = () => ControllerUnderTest.InitiatePayment(InputModel);
            var message =
                Assert.Throws<InvalidOperationException>(
                    testDelegate,
                    "There are products attached to this payment initiation.").Message;

            message.Should().Be("There are no products to purchase attached to this payment initiation.");
        }
    }
}