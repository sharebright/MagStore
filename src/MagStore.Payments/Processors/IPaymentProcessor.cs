using MagStore.Payments.Messages;
using MagStore.Payments.Providers;

namespace MagStore.Payments.Processors
{
    public interface IPaymentProcessor
    {
        IAuthResponse Authorise(IAuthRequest authRequest);
    }
}