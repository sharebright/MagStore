using System.Collections.Generic;
using RavenDBMembership.Infrastructure.Interfaces;

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
            ravenRepository.Add(entity);
            ravenRepository.Save();
        }

        public T Load(string id)
        {
            return ravenRepository.Load<T>(id);
        }

        public IList<T> Project()
        {
            return ravenRepository.Project<T>();
        }
    }
}
