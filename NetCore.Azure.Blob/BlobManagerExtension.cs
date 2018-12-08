using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeTypes;
using NetCore.Azure.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Blob.Core
{
    public static class BlobManagerExtension
    {

        public static async Task<string> UploadIFormFileAsync(this CloudBlobContainer container, IFormFile Image)
        {

            string extension = Path.GetExtension(Image.FileName);
            string fileName = Guid.NewGuid().ToString("N") + extension;
            string fileType = MimeTypeMap.GetMimeType(extension);

            var blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.Properties.ContentType = fileType;
            var fileStream = Image.OpenReadStream();

            await blockBlob.UploadFromByteArrayAsync(Util.ReadFully(fileStream, blockBlob.StreamWriteSizeInBytes),
                                0, (int)fileStream.Length);

            return fileName;
        }

        /// <summary>
        /// Adds an image to the specified container 
        /// </summary>
        /// <param name="ContainerName">Name of the container the code</param>
        /// <param name="Image"></param>
        /// <returns></returns>
        public static async Task<string> AddToContainerAsync(this IBlobManager blobManager,
                                string ContainerName, IFormFile Image)
            => await blobManager.GetContainer(ContainerName).UploadIFormFileAsync(Image);

    }
}
