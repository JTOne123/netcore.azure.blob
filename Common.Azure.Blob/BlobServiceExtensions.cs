using Common.Azure.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlobServiceExtensions
    {

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, Action<BlobManagerOptions> options)
            => services.Configure(options).AddSingleton<IBlobManager, BlobManager>();

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<BlobManagerOptions>(configuration).AddSingleton<IBlobManager, BlobManager>();

    }
}
