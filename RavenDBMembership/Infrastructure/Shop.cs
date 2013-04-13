using System;
using Raven.Client;
using RavenDBMembership.Infrastructure.Interfaces;
using RavenDbMembership.Infrastructure;

namespace RavenDBMembership.Infrastructure
{
    public class Shop : IShop
    {
        private readonly IRepository finder;

        public Shop( IRepository finder )
       {
           this.finder = finder;
//            dependencyRegister = new DependencyRegister();
//            dependencyRegister.Initialise();
//            UserCoordinator = dependencyRegister.Resolve<IUserCoordinator>();
//            RavenRepository = dependencyRegister.Resolve<IRepository>();
       }

        public ICoordinator<T> GetCoordinator<T>() where T : IRavenEntity
        {
//            var ravenRepository = dependencyRegister.Resolve<IRepository>() as RavenRepository;
//            return new Coordinator<T>(ravenRepository);
            return new Coordinator<T>(finder);
            throw new NotImplementedException();
        }

        public IUserCoordinator UserCoordinator { get; set; }
        public IDocumentStore DocumentStore { get; set; }
        public IRepository RavenRepository { get; set; }
    }
}
