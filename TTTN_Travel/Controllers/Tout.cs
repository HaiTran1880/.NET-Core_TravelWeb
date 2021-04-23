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
    public class Tout : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public Tout(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.ListTout = this.tourReContext.Tout.ToList().ToPagedList(pageNumber, 6);
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var tout = await this.tourReContext.Tout
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.listTout = this.tourReContext.Tout.Take(3);
            if (tout == null)
            {
                return NotFound();
            }

            return View(tout);
        }

        public IActionResult Book()
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
