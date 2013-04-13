using System;
using RavenDBMembership.Entities;

namespace RavenDBMembership.Infrastructure.Interfaces
{
    public interface IUserCoordinator
    {
        void SaveUser(User user);
        User LoadUser(Guid id);
    }
}