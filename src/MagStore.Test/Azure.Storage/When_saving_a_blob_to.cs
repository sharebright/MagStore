using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace MagStore.Test.Azure.Storage
{
    [TestFixture]
    public class When_saving_a_blob_to : An_azure_storage_account_with_a_resources_container
    {
        private const string FileName = @"barcode.png";
        private StreamReader inputStream;

        [SetUp]
        public void SetUp()
        {
            const string filePath = @"C:\temp\";

            inputStream = new StreamReader(filePath + FileName, true);

        }

        [Test]
        public void There_should_be_one_blob_in_the_container()
        {
            StorageAccessor.AddBlobToResource(FileName, inputStream.BaseStream);
            StorageAccessor.Resources.ListBlobs().Should().NotBeEmpty();
        }
    }
}