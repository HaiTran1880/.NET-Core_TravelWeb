using Microsoft.AspNetCore.Http;
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
            ViewBag.User = HttpContext.Session.GetString("name");
            ViewBag.Ten = HttpContext.Session.GetString("ten");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Ad = HttpContext.Session.GetString("adress");
            var pageNumber = page ?? 1;
            ViewBag.ListTout = this.tourReContext.Tout.ToList().ToPagedList(pageNumber, 6);
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {

            ViewBag.User = HttpContext.Session.GetString("name");
            ViewBag.Ten = HttpContext.Session.GetString("ten");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Ad = HttpContext.Session.GetString("adress");
            if (id == null)
            {
                ViewBag.User = HttpContext.Session.GetString("name");
                return NotFound();
            }

            var tout = await this.tourReContext.Tout
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.listTout = this.tourReContext.Tout.Take(3);
            if (tout == null)
            {
                ViewBag.User = HttpContext.Session.GetString("name");
                return NotFound();
            }

            return View(tout);
        }

        public IActionResult Book()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            ViewBag.Ten = HttpContext.Session.GetString("ten");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Phone = HttpContext.Session.GetString("phone");
            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.Ad = HttpContext.Session.GetString("adress");
            try
            {
                Dattour bill = new Dattour();
                bill.Hoten = Request.Form["your-name"];
                bill.Tentuor = Request.Form["tentour"];
                bill.Sdt = Request.Form["dienthoai"];
                bill.Dc = Request.Form["diachi"];
                bill.Email = Request.Form["your-email"];
                bill.Date = Convert.ToDateTime(Request.Form["khoihanh"]);
                bill.Songuoi = Convert.ToInt32(Request.Form["songuoi"]);
                bill.Treem = Convert.ToInt32(Request.Form["treem"]);
                bill.Ghichu = Request.Form["ghichu"];
                var tourBook = this.tourReContext.Tout.Where(x => x.Ten.Equals(bill.Tentuor)).FirstOrDefault();
                ViewBag.SoNg = Convert.ToInt32(Request.Form["songuoi"]);
                ViewBag.TreEM = Convert.ToInt32(Request.Form["treem"]);
                ViewBag.TenT = Request.Form["tentour"];

                string[] words = tourBook.Giakm.Split('.');
                string[] words1 = tourBook.Giatre.Split('.');
                string giaAdult = "";
                string giaChil = "";
                foreach (var word in words)
                {
                    giaAdult += word;
                }
                foreach (var word in words1)
                {
                    giaChil += word;
                }
                ViewBag.GiaT = string.Format("{0:#,##0}", tourBook.Giakm);
                ViewBag.GiaTr = string.Format("{0:#,##0}", tourBook.Giatre);
                ViewBag.TongT = string.Format("{0:#,##0}", Convert.ToDouble(giaAdult) * bill.Songuoi);
                ViewBag.TongTr = string.Format("{0:#,##0}", Convert.ToDouble(giaChil) * bill.Treem);
                ViewBag.TongAll = string.Format("{0:#,##0}", Convert.ToDouble(giaChil) * bill.Treem + Convert.ToDouble(giaAdult) * bill.Songuoi);
                bill.Thanhtien = string.Format("{0:#,##0}", (Convert.ToDouble(giaChil) * bill.Treem + Convert.ToDouble(giaAdult) * bill.Songuoi));
                bill.Trangthai = "Chưa thanh toán";
                ModelState.AddModelError("Error", "Check ID");
                this.tourReContext.Dattour.Add(bill);
                this.tourReContext.SaveChanges();


            }
            catch (Exception e)
            {

            }
            return View();
        }

        public IActionResult CancelBook()
        {
            try
            {

                string tenkh = HttpContext.Session.GetString("name");
                var tk = this.tourReContext.User.Where(x => x.Username.Equals(tenkh)).FirstOrDefault();
                //string tentour = Request.Form["tentour"];
                var deltour = this.tourReContext.Dattour.Where(x => x.Hoten.Equals(tk.Name)).FirstOrDefault();
                this.tourReContext.Dattour.Remove(deltour);
                this.tourReContext.SaveChanges();

            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
