using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SagePayMvc;

namespace MagStore.Test.Payments
{
    [TestFixture]
    public class When_making_a_request_and_receiving_a_response : MakingPaymentsSetUpFixture
    {
        [Test]
        public void The_payment_processor_should_receive_an_auth_request()
        {
            PaymentProcessor.Received().Authorise(AuthRequest);
        }

        [Test]
        public void The_payment_processor_should_return_a_response()
        {
            AuthResponse.Should().NotBeNull();
        }

        [Test]
        public void The_transaction_registrar_should_receive_the_valid_request()
        {
            TransactionRegistrar.Received().Send(
                AuthRequest.Context,
                AuthRequest.TransactionId,
                Arg.Any<ShoppingBasket>(),
                AuthRequest.BillingAddress,
                AuthRequest.DeliveryAddress,
                AuthRequest.CustomerEmail);
        }
    }
}