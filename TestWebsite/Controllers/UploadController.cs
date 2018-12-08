using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Azure.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebsite.Models;

namespace TestWebsite.Controllers
{
    public class UploadController : Controller
    {
        private readonly IBlobManager blobManager;

        public UploadController(IBlobManager blobManager)
        {
            this.blobManager = blobManager;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(UploadViewModel model)
        {
            string fname = await blobManager.AddToContainerAsync("nuget", model.File);
            string fname2 = await blobManager.AddToContainerAsync("nuget", model.OtherFile);
            return View(model);
        }
    }
}
