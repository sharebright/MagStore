using MagStore.Payments.Messages;
using MagStore.Payments.Processors;

namespace MagStore.Payments.Providers
{
    public class SagePayPaymentProvider : IPaymentProvider
    {
        private readonly IPaymentProcessor processor;

        public SagePayPaymentProvider(IPaymentProcessor processor)
        {
            this.processor = processor;
        }

        public IAuthResponse MakePayment(IAuthRequest authRequest)
        {
            return processor.Authorise(authRequest);
        }
    }
}