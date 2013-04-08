using System;
using System.Collections.Generic;
using MagStore.Data.Interfaces;
using MagStore.Entities;

namespace MagStore.Data
{
    public class Coordinator<T> : ICoordinator<T> where T : IRavenEntity
    {
        private readonly RavenRepository ravenRepository;

        public Coordinator( RavenRepository ravenRepository )
        {
            this.ravenRepository = ravenRepository;
        }

        public void Save( T entity )
        {
            ravenRepository.Add( entity );
            ravenRepository.Save();
        }

        public T Load( Guid id )
        {
            return ravenRepository.Load<T>( id );
        }

        public IList<T> List()
        {
            return ravenRepository.List<T>();
        }
    }
}