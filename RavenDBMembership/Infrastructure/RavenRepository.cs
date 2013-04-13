using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Infrastructure
{
    public class RavenRepository : IRepository
    {
        private readonly IDocumentStore store;

        private IDocumentSession currentSession;

        public RavenRepository(IDocumentStore store)
        {
            this.store = store;
            BuildNewSession();
        }

        private void BuildNewSession()
        {
            currentSession = store.OpenSession();
        }

        public void ForceNewSession()
        {
            CurrentSession.Dispose();
            BuildNewSession();
        }

        public IDocumentSession CurrentSession
        {
            get { return currentSession; }
        }

        public T Load<T>(string id) where T : IRavenEntity
        {
            T load = currentSession.Load<T>(id);
            return load;
        }

        public int Count<T>() where T : IRavenEntity
        {
            return currentSession.Query<T>().Count();
        }

        public void Add<T>(T item) where T : IRavenEntity
        {
            CurrentSession.Store(item);
        }

        public void Delete<T>(T item) where T : IRavenEntity
        {
            CurrentSession.Delete(item);
        }

        public void Save()
        {
            CurrentSession.SaveChanges();
        }

        public IList<T> Project<T>()
        {
            return CurrentSession.Query<T>().ToList();
        }
    }
}