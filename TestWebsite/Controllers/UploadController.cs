using Microsoft.AspNetCore.Mvc;
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
        private readonly BlobManager blobManager;

        public UploadController(BlobManager blobManager)
        {
            this.blobManager = blobManager;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(UploadViewModel model)
        {
            string fname = await blobManager.AddToContainer("samples", model.File);
            string fname2 = await blobManager.AddToContainer("samples", model.OtherFile);
            return View(model);
        }
    }
}
