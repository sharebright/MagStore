using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class ShopSettings : IShopSettings
    {
        public ShopSettings()
        {
//            Id = "settings";
//            Name = string.Empty;
//            TagLine = string.Empty;
//            CurrencySymbol = string.Empty;
//            Logo = string.Empty;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string CurrencySymbol { get; set; }
        public string Logo { get; set; }
        public string CurrencyFormat { get; set; }
        public decimal CurrencyConversion { get; set; }
        public decimal DeliveryCharge { get; set; }
    }
}
