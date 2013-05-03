using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace MagStore.Infrastructure.Interfaces
{
    public interface IRepository
    {
        IDocumentSession CurrentSession { get; }
        void ForceNewSession();
        T Load<T>(string id) where T : IRavenEntity;
        IEnumerable<T> Load<T>(IEnumerable<string> ids) where T : IRavenEntity;
        int Count<T>() where T : IRavenEntity;
        void Add<T>(T item) where T : IRavenEntity;
        void Delete<T>(T item) where T : IRavenEntity;
        void Save();
        ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path);
        IList<T> List<T>();
        void SaveAndCommit<T>(T item) where T : IRavenEntity;
        IRavenQueryable<T> Query<T>();
    }
}