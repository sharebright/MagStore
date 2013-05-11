using System;
using System.Collections.Generic;
using MagStore.Entities;
using MagStore.Payments.Messages;
using SagePayMvc;

namespace MagStore.Payments.Processors
{
    public class SagePayPaymentProcessor : IPaymentProcessor
    {
        private readonly ITransactionRegistrar registrar;

        public SagePayPaymentProcessor(ITransactionRegistrar registrar)
        {
            this.registrar = registrar;
        }

        public SagePayPaymentProcessor()
        {
            
        }

        public ITransactionRegistrar Registrar { private get; set; }

        public IAuthResponse Authorise(IAuthRequest authRequest)
        {
            ValidateRequest(authRequest);

            TransactionRegistrationResponse t = registrar.Send(
                authRequest.Context,
                authRequest.TransactionId,
                ConvertToShoppingBasket(authRequest.Products),
                authRequest.BillingAddress,
                authRequest.DeliveryAddress,
                authRequest.CustomerEmail);

            return new SagePayAuthResponse();
        }

        private ShoppingBasket ConvertToShoppingBasket(IList<Product> products)
        {
            return default(ShoppingBasket); // new ShoppingBasket("Test");
        }

        private void ValidateRequest(IAuthRequest authRequest)
        {
            if (authRequest == null)
            {
                throw new ArgumentNullException("authRequest", "A valid request is required for SagePay Auth requests.");
            }
            if (authRequest.Context == null)
            {
                throw new ArgumentNullException("authRequest.Context",
                                                "The RequestContext is required for SagePay Auth requests.");
            }
            if (authRequest.TransactionId == null)
            {
                throw new ArgumentNullException("authRequest.TransactionId",
                                                "The TransactionId is required for SagePay Auth requests.");
            }
            if (authRequest.Products == null)
            {
                throw new ArgumentNullException("authRequest.Products",
                                                "The TransactionId is required for SagePay Auth requests.");
            }
            if (authRequest.CustomerEmail == null)
            {
                throw new ArgumentNullException("authRequest.CustomerEmail",
                                                "The CustomerEmail is required for SagePay Auth requests.");
            }
            if (authRequest.BillingAddress == null)
            {
                throw new ArgumentNullException("authRequest.BillingAddress",
                                                "The BillingAddress is required for SagePay Auth requests.");
            }
            if (authRequest.DeliveryAddress == null)
            {
                throw new ArgumentNullException("authRequest.DeliveryAddress",
                                                "The DeliveryAddress is required for SagePay Auth requests.");
            }
        }
    }
}