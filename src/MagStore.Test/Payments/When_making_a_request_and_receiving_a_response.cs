using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test.Payments
{
    [TestFixture]
    public class When_making_a_request_and_receiving_a_response : MakingPaymentsSetUpFixture
    {
        [Test]
        public void The_payment_processor_should_receive_an_auth_request()
        {
            paymentProcessor.Received().Authorise(Arg.Is(authRequest));
        }

        [Test]
        public void The_payment_processor_should_return_a_response()
        {
            authResponse.Should().NotBeNull();
        }
    }
}