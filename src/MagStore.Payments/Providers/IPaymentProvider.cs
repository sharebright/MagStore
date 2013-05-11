using MagStore.Payments.Messages;

namespace MagStore.Payments.Providers
{
    public interface IPaymentProvider
    {
        IAuthResponse MakePayment(IAuthRequest authRequest);
    }
}
