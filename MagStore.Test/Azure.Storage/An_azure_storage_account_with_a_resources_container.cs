using NUnit.Framework;

namespace MagStore.Test.Azure.Storage
{
    [TestFixture]
    public class An_azure_storage_account_with_a_resources_container : StorageAccountSetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            StorageAccessor.PrepareResourcesContainer();
        }
    }
}