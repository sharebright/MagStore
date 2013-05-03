using System.Collections.Generic;
using MagStore.Infrastructure.Interfaces;
using Raven.Client.Linq;

namespace MagStore.Infrastructure
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
            Save(new List<T> { entity });
        }

        public void Save(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ravenRepository.SaveAndCommit(entity);
            }
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

        public void Delete(T entity)
        {
            ravenRepository.Delete(entity);
        }
    }
}
