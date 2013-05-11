using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Web.Models.Shop
{
    public class ShopSettingsViewModel
    {
        public ShopSettingsViewModel(IShopSettings settings, ProductImage logo)
        {
            Id = settings.Id;
            Name = settings.Name;
            TagLine = settings.TagLine;
            CurrencySymbol = settings.CurrencySymbol;
            CurrencyFormat = settings.CurrencyFormat;
            CurrencyConversion = settings.CurrencyConversion;
            Logo = logo;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyFormat { get; set; }
        public decimal CurrencyConversion { get; set; }
        public ProductImage Logo { get; set; }
    }
}