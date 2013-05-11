using MagStore.Payments.Messages;
using MagStore.Payments.Providers;
using SagePayMvc;

namespace MagStore.Payments.Processors
{
    public interface IPaymentProcessor
    {
        IAuthResponse Authorise(IAuthRequest authRequest);
        ITransactionRegistrar Registrar { set; }
    }
}