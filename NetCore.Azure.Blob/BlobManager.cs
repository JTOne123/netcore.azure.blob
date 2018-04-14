using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
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
    public class BlobManager
    {
        private readonly BlobManagerOptions options;

        public BlobManager(IOptions<BlobManagerOptions> options)
        {
            this.options = options.Value;
        }


        internal CloudStorageAccount StorageAccount =>
            new CloudStorageAccount(new StorageCredentials(options.AccountName, options.Key, options.Key), false);

        // Create a blob client.
        internal CloudBlobClient BlobClient => StorageAccount.CreateCloudBlobClient();

        // Get a reference to a container 
        internal CloudBlobContainer Container(string ContainerName)
                                => BlobClient.GetContainerReference(ContainerName);

        /// <summary>
        /// Adds an image to the specified container 
        /// </summary>
        /// <param name="ContainerName"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        public async Task<string> AddToContainer(string ContainerName, IFormFile Image)
        {
            string extension = Path.GetExtension(Image.FileName);
            string fileName = Guid.NewGuid().ToString("N") + extension;
            string fileType = MimeTypeMap.GetMimeType(extension);

            var container = Container(ContainerName);

            var blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.Properties.ContentType = fileType;
            var fileStream = Image.OpenReadStream();

            await blockBlob.UploadFromByteArrayAsync(ReadFully(fileStream, blockBlob.StreamWriteSizeInBytes),
                                0, (int)fileStream.Length);

            return fileName;
        }

        static byte[] ReadFully(Stream input, int size)
        {
            byte[] buffer = new byte[size];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, size)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


    }

}
