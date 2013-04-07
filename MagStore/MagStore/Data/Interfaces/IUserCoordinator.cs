using System;
using MagStore.Entities;

namespace MagStore.Data.Interfaces
{
    public interface IUserCoordinator
    {
        void SaveUser(User user);
        User LoadUser(Guid id);
    }
}