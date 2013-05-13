namespace MagStore.Infrastructure.Interfaces
{
    public interface IShopSettings : IRavenEntity
    {
        string Name { get; set; }
        string TagLine { get; set; }
        string CurrencySymbol { get; set; }
        string Logo { get; set; }
        string CurrencyFormat { get; set; }
        decimal CurrencyConversion { get; set; }
        decimal DeliveryCharge { get; set; }
    }
}