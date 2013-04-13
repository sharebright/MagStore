using System;
using Raven.Client;

namespace MagStore.Data.Interfaces
{
    public interface IRepository
    {
        T Load<T>(Guid id) where T : IRavenEntity;
        void Add<T>(T item) where T : IRavenEntity;
        void Delete<T>(T item) where T : IRavenEntity;
        void Save();
        IDocumentSession CurrentSession { get; }
        int Count<T>() where T : IRavenEntity;
        void ForceNewSession();
    }
}