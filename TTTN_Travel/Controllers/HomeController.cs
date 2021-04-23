using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;
using TTTN_Travel.Models.Global;

namespace TTTN_Travel.Controllers
{
    public class HomeController : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public HomeController(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index()
        {
            //Take 3 tour
            ViewBag.tour = this.tourReContext.Tour.Take(3);
            //Take 3 tout
            ViewBag.tout = this.tourReContext.Tout.Take(3);
            //Take 4 news
            ViewBag.news = this.tourReContext.Tintuc.Take(4);
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public IActionResult Secur()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
