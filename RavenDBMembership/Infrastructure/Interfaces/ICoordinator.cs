using System.Collections.Generic;

namespace RavenDbMembership.Infrastructure.Interfaces
{
    public interface ICoordinator<T>
    {
        void Save( T entity );
        T Load( string id );
        IList<T> Project();
    }
}