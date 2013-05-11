using System;
using System.Linq.Expressions;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using Raven.Client.Document;

namespace MagStore.Infrastructure
{
    public class Shop : IShop
    {
        private readonly IRepository finder;

        public Shop(IRepository finder)
        {
            this.finder = finder;
        }

        public ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity
        {
            return new Coordinator<T>(finder);
        }

        public ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path)
        {
            return finder.Include(path);
        }

        public IShopSettings GetSettings()
        {
            var settings = finder.Load<ShopSettings>("settings");
            if (settings == null)
            {
                settings = new ShopSettings();
                settings.Id = "settings";
                settings.Name = string.Empty;
                settings.TagLine = string.Empty;
                settings.CurrencySymbol = string.Empty;
                settings.CurrencyFormat = "0.00";
                settings.CurrencyConversion = 0m;
                settings.Logo = string.Empty;
                finder.SaveAndCommit(settings);
            }
            return settings;
        }

        public void UpdateSettings(IShopSettings settings)
        {
            finder.SaveAndCommit(settings);
        }

        public IShopSettings Settings { get; set; }
    }
}
