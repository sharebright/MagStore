using System.Collections.Generic;

namespace RavenDBMembership.Infrastructure.Interfaces
{
    public interface ICoordinator<T>
    {
        void Save( T entity );
        T Load( string id );
        IList<T> Project();
    }
}