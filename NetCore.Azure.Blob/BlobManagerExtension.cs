using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Azure.Blob
{
    public static class BlobManagerExtension
    {

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, Action<BlobManagerOptions> options) 
            => services.Configure(options).AddSingleton<IBlobManager, BlobManager>();

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<BlobManagerOptions>(configuration).AddSingleton<IBlobManager, BlobManager>();


        public static async Task<string> UploadIFormFile(this CloudBlobContainer container, IFormFile Image)
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


    }
}
