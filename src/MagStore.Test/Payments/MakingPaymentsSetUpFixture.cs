using MagStore.Payments.Messages;
using MagStore.Payments.Processors;
using MagStore.Payments.Providers;
using NSubstitute;
using NUnit.Framework;
using SagePayMvc;

namespace MagStore.Test.Payments
{
    public class MakingPaymentsSetUpFixture
    {
        private SagePayPaymentProvider paymentProvider;
        protected IAuthRequest authRequest;
        protected IAuthResponse authResponse;
        protected IPaymentProcessor paymentProcessor;

        [SetUp]
        public virtual void SetUp()
        {
            Arrange();

            Act();
        }

        protected void Arrange()
        {
            paymentProcessor = Substitute.For<SagePayPaymentProcessor>();
            paymentProvider = new SagePayPaymentProvider(paymentProcessor);
            authRequest = Substitute.For<IAuthRequest>();
            authRequest.BillingAddress = Substitute.For<Address>();
            authRequest.DeliveryAddress = Substitute.For<Address>();
        }

        protected void Act()
        {
            authResponse = paymentProvider.MakePayment(authRequest);
        }
    }
}