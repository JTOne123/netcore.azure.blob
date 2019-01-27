using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Azure.Blob
{
    class BlobManager : IBlobManager
    {
        private readonly BlobManagerOptions options;

        public BlobManager(IOptions<BlobManagerOptions> options)
        {
            this.options = options.Value;
        }

        public CloudStorageAccount GetStorageAccount() 
            => new CloudStorageAccount(new StorageCredentials(options.AccountName, options.Key, options.Key), false);

        // Create a blob client.
        public CloudBlobClient GetBlobClient() => GetStorageAccount().CreateCloudBlobClient();

        // Get a reference to a container 
        public CloudBlobContainer GetContainer(string ContainerName)
                                => GetBlobClient().GetContainerReference(ContainerName);



    }

}
