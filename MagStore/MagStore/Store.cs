using MagStore.Data;
using MagStore.Data.Interfaces;
using Raven.Client;

namespace MagStore
{
    public class Store
    {
        private readonly DependencyRegister dependencyRegister;

        public Store( IRavenCredentials ravenCredentials )
        {
            dependencyRegister = new DependencyRegister( ravenCredentials);
            dependencyRegister.Initialise();
            UserCoordinator = dependencyRegister.Resolve<IUserCoordinator>();
            RavenRepository = dependencyRegister.Resolve<IRepository>();
        }

        public ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity
        {
            var ravenRepository = dependencyRegister.Resolve<IRepository>() as RavenRepository;
            return new Coordinator<T>(ravenRepository);
        }

        public IUserCoordinator UserCoordinator { get; set; }
        public IDocumentStore DocumentStore { get; set; }
        public IRepository RavenRepository { get; set; }
    }
}
