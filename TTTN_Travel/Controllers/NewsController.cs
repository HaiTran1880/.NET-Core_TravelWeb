using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;

namespace TTTN_Travel.Controllers
{
    public class NewsController : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public NewsController(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            return View();
        }
        public IActionResult Detail(int id)
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            var it = this.tourReContext.Tintuc.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.tin = it;   
            ViewBag.listNews = this.tourReContext.Tintuc.Take(8);
            return View(it);
        }
    }
}
