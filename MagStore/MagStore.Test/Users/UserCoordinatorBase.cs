using Castle.Windsor;
using MagStore.Infrastructure;
using MagStore.Infrastructure.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test.Users
{
    [TestFixture]
    public class UserCoordinatorBase
    {
        [SetUp]
        public void SetUp()
        {
//            var rc = Substitute.For<IRavenCredentials>();
//            rc.ApiKey.Returns( "b23f5154-30b0-48f8-b619-76ee77d0234d" );
//            rc.Url.Returns( "https://ec2-eu4.cloudbird.net/databases/c818ddc6-dc4b-4b57-a439-4329fff0e61b.rdbtest-mag" );
//            IWindsorContainer container = Substitute.For<WindsorContainer>();
//            Shop = new Shop(Substitute.For<IRepository>()  );
        }

        protected Shop Shop { get; set; }
    }
}
