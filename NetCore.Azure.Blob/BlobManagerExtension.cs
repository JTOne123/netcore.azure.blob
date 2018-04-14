using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Azure.Blob
{
    public static class BlobManagerExtension
    {

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, Action<BlobManagerOptions> options) 
            => services.Configure(options).AddSingleton<BlobManager>();

        public static IServiceCollection AddAzureBlob(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<BlobManagerOptions>(configuration).AddSingleton<BlobManager>();


    }
}
