using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models.Global;
using static System.Net.Mime.MediaTypeNames;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
       
        [Area("Admin")]
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            return View();
        }
        
    } 

}
