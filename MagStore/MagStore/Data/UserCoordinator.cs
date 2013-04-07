using System;
using MagStore.Data.Interfaces;
using MagStore.Entities;
using Raven.Client;

namespace MagStore.Data
{
    public class UserCoordinator : IUserCoordinator
    {
        private readonly IRepository ravenRepository;

        public UserCoordinator( IRepository ravenRepository )
        {
            this.ravenRepository = ravenRepository;
        }

        public void SaveUser( User user )
        {
            ravenRepository.Add( user );
            ravenRepository.Save();
        }

        public User LoadUser( Guid id )
        {
            return ravenRepository.Load<User>( id );
        }
    }
}