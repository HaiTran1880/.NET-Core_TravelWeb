using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;
using X.PagedList;

namespace TTTN_Travel.Controllers
{
    public class ToursController : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public ToursController(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index(int ? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.ListTour = this.tourReContext.Tour.ToList().ToPagedList(pageNumber,6);
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {

            ViewBag.listTour = this.tourReContext.Tour.Take(3);
            if (id == null)
            {
                return NotFound();
            }
            var tour = await this.tourReContext.Tour
                .FirstOrDefaultAsync(m => m.Idtour == id);
            
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        public IActionResult Booking()
        {
            Dattour bill = new Dattour();
            bill.Hoten = Request.Form["your-name"];
            bill.Tentuor = Request.Form["tentour"];
            bill.Sdt = Request.Form["dienthoai"];
            bill.Dc = Request.Form["diachi"];
            bill.Email = Request.Form["your-email"];
            bill.Date = Convert.ToDateTime(Request.Form["khoihanh"]);
            bill.Songuoi = Convert.ToInt32(Request.Form["songuoi"]);
            bill.Ghichu = Request.Form["ghichu"];
            ModelState.AddModelError("Error", "Check ID");
            this.tourReContext.Dattour.Add(bill);
            this.tourReContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
