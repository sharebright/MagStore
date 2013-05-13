using System.Web;

namespace MagStore.Web.Models.Shop
{
    public class ChangeShopSettingsPostInputModel
    {
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyFormat { get; set; }
        public decimal CurrencyConversion { get; set; }
        public decimal DeliveryCharge { get; set; }
        public HttpPostedFileBase Logo { get; set; }
    }
}