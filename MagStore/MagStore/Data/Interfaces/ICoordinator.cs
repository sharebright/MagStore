using System;
using System.Collections.Generic;
using MagStore.Entities;

namespace MagStore.Data.Interfaces
{
    public interface ICoordinator<T>
    {
        void Save( T entity );
        T Load( Guid id );
        IList<T> List();
    }
}