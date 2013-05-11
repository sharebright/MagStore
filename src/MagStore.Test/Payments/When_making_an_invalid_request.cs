using System;
using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Payments
{
    [TestFixture]
    public class When_making_an_invalid_request : MakingPaymentsSetUpFixture
    {
        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_that_is_null()
        {
            AuthRequest = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                    () => PaymentProcessor.Authorise(AuthRequest),
                    "An null request is required to make this test valid.")
                .ParamName;

            paramName.Should().Be("authRequest");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_null_context()
        {
            AuthRequest.Context = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null context is required to make this test valid.")
                    .ParamName;
            
            paramName.Should().Be("authRequest.Context");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_null_transaction_id()
        {
            AuthRequest.TransactionId = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null transaction id is required to make this test valid.")
                    .ParamName;

            paramName.Should().Be("authRequest.TransactionId");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_a_null_product_list()
        {
            AuthRequest.Products = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null product list is required to make this test valid.")
                    .ParamName;

            paramName.Should().Be("authRequest.Products");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_a_null_customer_email()
        {
            AuthRequest.CustomerEmail = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null customer email is required to make this test valid.")
                    .ParamName;

            paramName.Should().Be("authRequest.CustomerEmail");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_a_null_billing_address()
        {
            AuthRequest.BillingAddress = null;
            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null billing address is required to make this test valid.")
                    .ParamName;

            paramName.Should().Be("authRequest.BillingAddress");
        }

        [Test]
        public void The_payment_processor_should_throw_argumentnullexception_for_an_auth_request_with_a_null_delivery_address()
        {
            AuthRequest.DeliveryAddress = null;

            var paramName =
                Assert.Throws<ArgumentNullException>(
                        () => PaymentProcessor.Authorise(AuthRequest),
                        "A null delivery address is required to make this test valid.")
                    .ParamName;

            paramName.Should().Be("authRequest.DeliveryAddress");
        }
    }
}