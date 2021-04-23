using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass= HttpContext.Session.GetString("pass");
            return View();
        }
    }
}
