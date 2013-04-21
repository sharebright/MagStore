using System;
using System.Linq.Expressions;
using Raven.Client.Document;
using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Infrastructure
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
    }
}
