using System.Collections.Generic;
using System.Web.Routing;
using MagStore.Entities;
using SagePayMvc;

namespace MagStore.Payments.Messages
{
    public class SagepayAuthRequest : IAuthRequest
    {
        public IDictionary<string, string> DataPairs { get; set; }

        public RequestContext Context { get; set; }
        public string TransactionId { get; set; }
        public IList<Product> Products { get; set; }
        public string CustomerEmail { get; set; }
        public Address BillingAddress { get; set; }
        public Address DeliveryAddress { get; set; }

        public SagepayAuthRequest(string vendorTxCode, string amount, string currency, string description,
            string successUrl, string failureUrl, string billingSurname, string billingFirstNames,
            string billingAddress1, string billingCity, string billingPostCode, string billingCountry,
            string deliverySurname, string deliveryFirstNames, string deliveryAddress1,
            string deliveryCity, string deliveryPostCode, string deliveryCountry)
        {
            DataPairs = new Dictionary<string, string>
            {
                {"VendorTxCode", vendorTxCode},
                {"Amount", amount},
                {"Currency", currency},
                {"Description", description},
                {"SuccessURL", successUrl},
                {"FailureURL", failureUrl},
                {"BillingSurname", billingSurname},
                {"BillingFirstnames", billingFirstNames},
                {"BillingAddress1", billingAddress1},
                {"BillingCity", billingCity},
                {"BillingPostCode", billingPostCode},
                {"BillingCountry", billingCountry},
                {"DeliverySurname", deliverySurname},
                {"DeliveryFirstnames", deliveryFirstNames},
                {"DeliveryAddress1", deliveryAddress1},
                {"DeliveryCity", deliveryCity},
                {"DeliveryPostCode", deliveryPostCode},
                {"DeliveryCountry", deliveryCountry}
            };
            
            BillingAddress = new Address
            {
                Address1 = billingAddress1,
                City = billingCity,
                PostCode = billingPostCode,
                Country = billingCountry,
                Firstnames = billingFirstNames,
                Surname = billingSurname
            };
            
            DeliveryAddress = new Address
            {
                Address1 = deliveryAddress1,
                City = deliveryCity,
                PostCode = deliveryPostCode,
                Country = deliveryCountry,
                Firstnames = deliveryFirstNames,
                Surname = deliverySurname
            };
        }

        // optional
        public string CustomerName { set { UpsertVariable(value, "CustomerName"); } }
        public string CustomerEMail { set { UpsertVariable(value, "CustomerEMail"); } }
        public string VendorEMail { set { UpsertVariable(value, "VendorEMail"); } }
        public string EMailMessage { set { UpsertVariable(value, "CustomerEMail"); } }
        public string BillingAddress2 { set { UpsertVariable(value, "BillingAddress2"); } }
        public string BillingState { set { UpsertVariable(value, "BillingState"); } }
        public string BillingPhone { set { UpsertVariable(value, "BillingPhone"); } }
        public string DeliveryAddress2 { set { UpsertVariable(value, "DeliveryAddress2"); } }
        public string DeliveryState { set { UpsertVariable(value, "DeliveryState"); } }
        public string DeliveryPhone { set { UpsertVariable(value, "DeliveryPhone"); } }
        public string Basket { set { UpsertVariable(value, "Basket"); } }
        public string AllowGiftAid { set { UpsertVariable(value, "AllowGiftAid"); } }
        public string ApplyAvscv2 { set { UpsertVariable(value, "ApplyAVSCV2"); } }
        public string Apply3DSecure { set { UpsertVariable(value, "Apply3DSecure"); } }
        public string BillingAgreement { set { UpsertVariable(value, "BillingAgreement"); } }
        public string BasketXml { set { UpsertVariable(value, "BasketXML"); } }
        public string CustomerXml { set { UpsertVariable(value, "CustomerXML"); } }
        public string SurchargeXml { set { UpsertVariable(value, "SurchargeXML"); } }
        public string VendorData { set { UpsertVariable(value, "VendorData"); } }
        public string ReferrerId { set { UpsertVariable(value, "ReferrerID"); } }
        public string Language { set { UpsertVariable(value, "Language"); } }
        public string Website { set { UpsertVariable(value, "Website"); } }

        private void UpsertVariable(string value, string variable)
        {
            if (DataPairs.ContainsKey(variable))
            {
                DataPairs[variable] = value;
            }
            else
            {
                DataPairs.Add(variable, value);
            }
        }
    }
}