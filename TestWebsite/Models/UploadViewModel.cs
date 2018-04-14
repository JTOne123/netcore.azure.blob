using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebsite.Models
{
    public class UploadViewModel
    {

        public string Title { get; set; }

        public IFormFile File { get; set; }

        public IFormFile OtherFile { get; set; }

    }
}
