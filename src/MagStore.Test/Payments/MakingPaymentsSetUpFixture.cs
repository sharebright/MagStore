using MagStore.Payments.Messages;
using MagStore.Payments.Processors;
using MagStore.Payments.Providers;
using NSubstitute;
using SagePayMvc;

namespace MagStore.Test.Payments
{
    public class MakingPaymentsSetUpFixture : TestSetUpFixture
    {
        private IPaymentProvider paymentProvider;
        protected IAuthRequest AuthRequest;
        protected IAuthResponse AuthResponse;
        protected IPaymentProcessor PaymentProcessor;
        protected ITransactionRegistrar TransactionRegistrar;

        protected override void Arrange()
        {
            TransactionRegistrar = Substitute.For<ITransactionRegistrar>();
            PaymentProcessor = Substitute.For<SagePayPaymentProcessor>(TransactionRegistrar);
            paymentProvider = new SagePayPaymentProvider(PaymentProcessor);
            AuthRequest = Substitute.For<IAuthRequest>();
            AuthRequest.BillingAddress = Substitute.For<Address>();
            AuthRequest.DeliveryAddress = Substitute.For<Address>();
        }

        protected override void Act()
        {
            AuthResponse = paymentProvider.MakePayment(AuthRequest);
        }
    }
}