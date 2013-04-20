using System;
using RavenDbMembership.Entities;

namespace RavenDbMembership.Infrastructure.Interfaces
{
    public interface IUserCoordinator
    {
        void SaveUser(User user);
        User LoadUser(Guid id);
    }
}