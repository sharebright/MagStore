using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace MagStore.Infrastructure
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
            T load;
            try
            {
                load = CurrentSession.Load<T>(id);
            }
            catch (InvalidOperationException e)
            {
                ForceNewSession();
                load = CurrentSession.Load<T>(id);
            }
            CurrentSession.Dispose();
            return load;
        }

        public IEnumerable<T> Load<T>(IEnumerable<string> id) where T : IRavenEntity
        {
            IEnumerable<T> load;
            try
            {
                load = CurrentSession.Load<T>(id);
            }
            catch (InvalidOperationException e)
            {
                ForceNewSession();
                load = CurrentSession.Load<T>(id);
            }
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
            CurrentSession.SaveChanges();
            session.Dispose();
        }

        public void Save()
        {
            CurrentSession.SaveChanges();
            session.Dispose();
        }

        public void SaveAndCommit<T>(T item) where T : IRavenEntity
        {
            try
            {
                CurrentSession.Store(item, item.Id);
                CurrentSession.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                ForceNewSession();
                CurrentSession.Store(item, item.Id);
                CurrentSession.SaveChanges();
            }
            CurrentSession.Dispose();
        }

        public IList<T> List<T>()
        {
            List<T> project;
            try
            {
                project = CurrentSession.Query<T>().ToList();
            }
            catch (InvalidOperationException e)
            {
                ForceNewSession();
                project = CurrentSession.Query<T>().ToList();
            }
            foreach (var product in project.Where(p => p.GetType() == typeof(Product))
                .Select(p => (p as Product))
                .Where(product => product != null))
            {
                product.AgeRange = product.AgeRange ?? new int[] {};
                product.Promotions = product.Promotions ?? new List<string>();
            }
            foreach (var promotion in project.Where(p => p.GetType() == typeof (Promotion))
                .Select(p => (p as Promotion))
                .Where(promotion => promotion != null))
            {
                promotion.Restrictions = promotion.Restrictions ?? new List<string>();
            }
            session.Dispose();
            return project;
        }

        public IRavenQueryable<T> Query<T>()
        {
            IRavenQueryable<T> query;
            try
            {
                query = CurrentSession.Query<T>();
            }
            catch (Exception)
            {
                ForceNewSession();
                query = CurrentSession.Query<T>();
            }
            return query;
        }

        public ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path)
        {
            return CurrentSession.Include(path);
        }
    }
}