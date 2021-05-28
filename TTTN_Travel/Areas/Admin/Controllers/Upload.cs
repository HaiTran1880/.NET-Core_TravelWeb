using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Upload : Controller
    {
        private IHostingEnviroment hostingEnviroment;
        /*public Upload(IHostingEnviroment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("upload_ckeditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadImg(IFormFile img)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + img.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), hostingEnviroment.WebRootPath, "images", fileName);
            var stream = new FileStream(path, FileMode.Create);
            img.CopyToAsync(stream);
            return new JsonResult(new { path = "/images/" + fileName });
        }*/
    }
}
