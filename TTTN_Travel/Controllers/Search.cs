using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;

namespace TTTN_Travel.Controllers
{
    public class Search : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public Search(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index()
        {
            string input = Request.Form["s"];
            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input) || x.Mota.Contains(input));
            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) || x.Chitiet.Contains(input));
            ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input)  || x.Chitiet.Contains(input));
           
            return View();
        }
    }
}
