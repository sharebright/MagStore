using MagStore.Data;
using NSubstitute;
using NUnit.Framework;

namespace MagStore.Test
{
    [TestFixture]
    public class UserCoordinatorBase
    {
        [SetUp]
        public void SetUp()
        {
            var rc = Substitute.For<IRavenCredentials>();
            rc.ApiKey.Returns( "b23f5154-30b0-48f8-b619-76ee77d0234d" );
            rc.Url.Returns( "https://ec2-eu4.cloudbird.net/databases/c818ddc6-dc4b-4b57-a439-4329fff0e61b.rdbtest-mag" );
            Store = new Store( rc );
        }

        protected Store Store { get; set; }
    }
}
