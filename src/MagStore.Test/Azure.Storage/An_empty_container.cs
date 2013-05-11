using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Azure.Storage
{
    [TestFixture]
    [Explicit]
    public class An_empty_container : From_the_azure_storage_account
    {
        [SetUp]
        public void SetUp()
        {
            StorageAccessor.PrepareResourcesContainer();
        }

        [Test]
        public void Should_exist()
        {
            StorageAccessor.Resources.Exists().Should().BeTrue();
        }

        [Test]
        public void Should_be_empty()
        {
            StorageAccessor.Resources.ListBlobs().Should().BeEmpty();
        }
    }
}