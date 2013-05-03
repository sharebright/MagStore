using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace MagStore.Azure
{
    public class StorageAccessor : IStorageAccessor
    {
        private readonly CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer resourcesContainer;

        public StorageAccessor(string accountName, string accessKey, bool useHttps = false)
        {
            var storageCredentials = new StorageCredentials(accountName, accessKey);
            storageAccount = new CloudStorageAccount(storageCredentials, useHttps);
            var cloudBlobContainer = GetContainerByName("resources");
            Resources = cloudBlobContainer.Exists() ? cloudBlobContainer : null;
        }

        public CloudBlobClient GetBlobClient()
        {
            return blobClient ?? (blobClient = storageAccount.CreateCloudBlobClient());
        }

        public Uri AddBlobToResource(string fileName, Stream inputStream)
        {
            var createdBlob = CreateBlob(fileName, inputStream);
            var createdBlobUri = createdBlob.Uri;
            return createdBlobUri;
        }

        public CloudBlobContainer Resources
        {
            get { return resourcesContainer; }
            private set { resourcesContainer = value; }
        }

        private CloudBlobContainer GetContainerByName(string containerName)
        {
            return GetBlobClient().GetContainerReference(containerName);
        }

        private CloudBlockBlob CreateBlob(string fileName, Stream inputStream)
        {
            PrepareResourcesContainer();

            var blob = resourcesContainer.GetBlockBlobReference(fileName);

            blob.UploadFromStream(inputStream);

            return blob;
        }

        public void PrepareResourcesContainer()
        {
            if (ResourcesContainerExists()) return;
            resourcesContainer = GetBlobClient().GetContainerReference("resources");
            var requestOptions = new BlobRequestOptions {RetryPolicy = new ExponentialRetry()};

            resourcesContainer.CreateIfNotExists(requestOptions, null);
            var permissions = resourcesContainer.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            resourcesContainer.SetPermissions(permissions);
        }

        private bool ResourcesContainerExists()
        {
            return GetContainerByName("resources").Exists();
        }
    }
}