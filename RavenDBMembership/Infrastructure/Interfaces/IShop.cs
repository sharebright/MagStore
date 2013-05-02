using System;
using System.Linq.Expressions;
using Raven.Client.Document;

namespace MagStore.Infrastructure.Interfaces
{
    public interface IShop
    {
        ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity;
        ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path);
    }
}