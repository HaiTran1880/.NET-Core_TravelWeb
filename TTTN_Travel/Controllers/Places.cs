using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;

namespace TTTN_Travel.Controllers
{
    public class Places : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public Places(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            ViewBag.listNews = this.tourReContext.Tintuc.Take(8);
            return View();
        }
        public IActionResult LocalPlace()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            return View();
        }
    }
}
