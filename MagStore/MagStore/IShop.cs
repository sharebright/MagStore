using MagStore.Data.Interfaces;
using Raven.Client;

namespace MagStore
{
    public interface IShop
    {
        ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity;
        IUserCoordinator UserCoordinator { get; set; }
        IDocumentStore DocumentStore { get; set; }
        IRepository RavenRepository { get; set; }
    }
}