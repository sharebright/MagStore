using Raven.Client;

namespace RavenDBMembership.Infrastructure.Interfaces
{
    public interface IShop
    {
        ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity;
    }
}