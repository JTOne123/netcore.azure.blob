using Microsoft.AspNetCore.Http;
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

namespace NetCore.Azure.Blob
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

        /// <summary>
        /// Adds an image to the specified container 
        /// </summary>
        /// <param name="ContainerName"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        public async Task<string> AddToContainer(string ContainerName, IFormFile Image)
            => await GetContainer(ContainerName).UploadIFormFile(Image);

        public async Task<string> AddToContainer(string ContainerName, string Content)
        {
            string fileName = Guid.NewGuid().ToString("N");
            await AddToContainer(ContainerName, fileName, Content);
            return fileName;
        }

        public async Task AddToContainer(string ContainerName, string FileName, string Content)
            => await GetBlockBlobReference(ContainerName, FileName).UploadTextAsync(Content);

        public CloudBlockBlob GetBlockBlobReference(string ContainerName, string BlobName)
            => GetContainer(ContainerName).GetBlockBlobReference(BlobName);
    }

}
