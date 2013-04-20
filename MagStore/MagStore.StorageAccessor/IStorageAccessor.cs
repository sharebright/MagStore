using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MagStore.Azure
{
    public interface IStorageAccessor
    {
        CloudBlobClient GetBlobClient();
        Uri AddBlobToResource(string fileName, Stream inputStream);
        CloudBlobContainer Resources { get; }
        void PrepareResourcesContainer();
    }
}