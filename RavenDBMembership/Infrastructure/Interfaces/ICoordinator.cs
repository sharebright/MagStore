using System.Collections.Generic;
using Raven.Client.Linq;

namespace RavenDbMembership.Infrastructure.Interfaces
{
    public interface ICoordinator<T>
    {
        void Save( T entity );
        void Save(IEnumerable<T> entity);
        T Load(string id);
        IEnumerable<T> Load( IEnumerable<string> ids );
        IList<T> List();
        IRavenQueryable<T> Query<T>();
        void Delete(T entity);
    }
}