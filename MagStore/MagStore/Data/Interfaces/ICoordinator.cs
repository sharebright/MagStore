using System;

namespace MagStore.Data.Interfaces
{
    public interface ICoordinator<T>
    {
        void Save( T entity );
        T Load( Guid id );
    }
}