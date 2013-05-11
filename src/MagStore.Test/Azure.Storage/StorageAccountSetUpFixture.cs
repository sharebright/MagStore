using System.Configuration;
using FluentAssertions;
using MagStore.Azure;
using NUnit.Framework;

namespace MagStore.Test.Azure.Storage
{
    public class StorageAccountSetUpFixture
    {
        protected IStorageAccessor StorageAccessor { get; private set; }

        [TestFixtureSetUp]
        public void SetUp()
        {
            var accountName = ConfigurationManager.AppSettings["StorageAccount"];
            var accessKey = ConfigurationManager.AppSettings["StorageAccessKey"];

            StorageAccessor = new StorageAccessor(accountName, accessKey);

            StorageAccessor.Resources.Should().BeNull();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (StorageAccessor.Resources != null && StorageAccessor.Resources.Exists())
            {
                StorageAccessor.Resources.Delete();
            }
        }
    }
}