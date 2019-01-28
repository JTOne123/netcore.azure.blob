using Common.Azure.Blob;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Azure.Blob.TagHelpers
{

    [HtmlTargetElement("img", Attributes = "azure-src")]
    public class AzureSrcTagHelper : TagHelper
    {
        private readonly IOptions<BlobManagerOptions> options;

        public AzureSrcTagHelper(IOptions<BlobManagerOptions> options)
        {
            
            this.options = options;
        }

        public string AzureSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("src", 
                   $"https://{options.Value.AccountName}.blob.core.windows.net/{options.Value.PathPrefix}{AzureSrc}");

            base.Process(context, output);
        }

    }
}
