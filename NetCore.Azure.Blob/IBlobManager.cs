using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Azure.Blob
{
    public interface IBlobManager
    {
        CloudStorageAccount GetStorageAccount();

        CloudBlobClient GetBlobClient();

        CloudBlobContainer GetContainer(string ContainerName);

        Task<string> AddToContainer(string ContainerName, IFormFile Image);

    }
}
