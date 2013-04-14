using System;
using System.Collections.Generic;
using Raven.Client;

namespace RavenDBMembership.Infrastructure.Interfaces
{
    public interface IRepository
    {
        IDocumentSession CurrentSession { get; }
        void ForceNewSession();
        T Load<T>(string id) where T : IRavenEntity;
        int Count<T>() where T : IRavenEntity;
        void Add<T>(T item) where T : IRavenEntity;
        void Delete<T>(T item) where T : IRavenEntity;
        void Save();
        IList<T> Project<T>();
        void SaveAndCommit<T>(T item) where T : IRavenEntity;
    }
}