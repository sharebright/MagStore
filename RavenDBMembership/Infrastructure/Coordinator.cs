using System.Collections.Generic;
using Raven.Client.Linq;
using RavenDbMembership.Infrastructure.Interfaces;

namespace RavenDbMembership.Infrastructure
{
    public class Coordinator<T> : ICoordinator<T> where T : IRavenEntity
    {
        private readonly IRepository ravenRepository;

        public Coordinator(IRepository ravenRepository)
        {
            this.ravenRepository = ravenRepository;
        }

        public void Save(T entity)
        {
            ravenRepository.SaveAndCommit(entity);
        }

        public T Load(string id)
        {
            return ravenRepository.Load<T>(id);
        }

        public IEnumerable<T> Load(IEnumerable<string> ids)
        {
            return ravenRepository.Load<T>(ids);
        }

        public IList<T> List()
        {
            return ravenRepository.List<T>();
        }

        public IRavenQueryable<T> Query<T>()
        {
            return ravenRepository.Query<T>();
        }
    }
}
