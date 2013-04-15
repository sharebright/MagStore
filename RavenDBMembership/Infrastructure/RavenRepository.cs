using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Client;
using RavenDBMembership.Entities;
using RavenDBMembership.Infrastructure.Interfaces;

namespace RavenDBMembership.Infrastructure
{
    public class RavenRepository : IRepository
    {
        private readonly IDocumentStore store;

        private IDocumentSession session;

        public RavenRepository(IDocumentStore store)
        {
            this.store = store;
        }

        private void BuildNewSession()
        {
            session = store.OpenSession();
        }

        public void ForceNewSession()
        {
            CurrentSession.Dispose();
            BuildNewSession();
        }

        public IDocumentSession CurrentSession
        {
            get { return session ?? (session = store.OpenSession()); }
        }

        public T Load<T>(string id) where T : IRavenEntity
        {
            T load = CurrentSession.Load<T>(id);
            CurrentSession.Dispose();
            return load;
        }

        public int Count<T>() where T : IRavenEntity
        {
            var count = session.Query<T>().Count();
            session.Dispose();
            return count;
        }

        public void Add<T>(T item) where T : IRavenEntity
        {
            CurrentSession.Store(item);
        }

        public void Delete<T>(T item) where T : IRavenEntity
        {
            CurrentSession.Delete(item);
            session.Dispose();
        }

        public void Save()
        {
            CurrentSession.SaveChanges();
            session.Dispose();
        }

        public void SaveAndCommit<T>(T item) where T : IRavenEntity
        {
            CurrentSession.Store(item, item.Id);
            CurrentSession.SaveChanges();
            CurrentSession.Dispose();
        }

        public IList<T> Project<T>()
        {
            var project = CurrentSession.Query<T>().ToList();
            foreach (var promotion in project.Where(p => p.GetType() == typeof (Promotion))
                .Select(p => (p as Promotion))
                .Where(promotion => promotion != null))
            {
                promotion.Restrictions = promotion.Restrictions ?? new List<Promotion>();
            }
            session.Dispose();
            return project;
        }

        public IList<T> IncludeAndProject<T>(Expression<Func<T, object>> path)
        {
            return CurrentSession.Query<T>().Include<T>(path).ToList();
        }
    }
}