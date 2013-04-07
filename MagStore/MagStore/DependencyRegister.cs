using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MagStore.Data;
using MagStore.Data.Interfaces;
using MagStore.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace MagStore
{
    internal class DependencyRegister
    {
        private readonly WindsorContainer container = new WindsorContainer();

        public DependencyRegister( IRavenCredentials ravenCredentials )
        {
            RavenCredentials = ravenCredentials;
        }

        public IRavenCredentials RavenCredentials { get; set; }

        public void Initialise()
        {
            RegisterComponents();
        }
        private void RegisterComponents()
        {
            container.Register( Classes.FromThisAssembly()
                            .BasedOn<IRavenEntity>()
                            .LifestyleTransient() );

            container.Register( Component.For<IRepository>().ImplementedBy<RavenRepository>().LifeStyle.Singleton );
            container.Register( Component.For<IUserCoordinator>().ImplementedBy<UserCoordinator>().LifeStyle.Singleton );
            container.Register( Classes.FromAssemblyContaining<RavenRepository>()
                        .BasedOn( typeof( ICoordinator<> ) )
                        .WithService.AllInterfaces().LifestyleSingleton() );

            var registrations = Component.For<IDocumentStore>().ImplementedBy<DocumentStore>()
                                         .DependsOn(new
                                             {
                                                 RavenCredentials.Url, 
                                                 RavenCredentials.ApiKey
                                             }).OnCreate(DoRavenInitialisation).LifeStyle.Singleton;
            container.Register( registrations );
        }

        private static void DoRavenInitialisation( IKernel kernel, IDocumentStore store )
        {
            store.Initialize();
        }

        internal T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
