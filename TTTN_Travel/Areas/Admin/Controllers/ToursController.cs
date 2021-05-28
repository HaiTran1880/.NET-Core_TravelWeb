using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTTN_Travel.Models;
using TTTN_Travel.Models.Global;
using X.PagedList;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ToursController : Controller
    {
        private readonly TourReContext _context;

        public ToursController(TourReContext context)
        {
            _context = context;
        }

        // GET: Admin/Tours
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            return View(await _context.Tour.ToListAsync());
        }

        // GET: Admin/Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Idtour == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Admin/Tours/Create
        public IActionResult Create()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            return View();
        }

        // POST: Admin/Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tentuor,ImageFile,Ngaybd,Ngaykt,Mota,Gia,Quocgia,Trongnuoc,Idtour,Lichtrinh,Danhgia")] Tour tour)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (ModelState.IsValid)
            {
                UploadFiles.ImgURL(tour.ImageFile);
                tour.Image = tour.ImageFile.FileName;
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Admin/Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tentuor,ImageFile,Ngaybd,Ngaykt,Mota,Gia,Quocgia,Trongnuoc,Idtour,Lichtrinh,Danhgia")] Tour tour)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id != tour.Idtour)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFiles.ImgURL(tour.ImageFile);
                    tour.Image = tour.ImageFile.FileName;
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Idtour))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Admin/Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Idtour == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Admin/Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            var tour = await _context.Tour.FindAsync(id);
            _context.Tour.Remove(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tour.Any(e => e.Idtour == id);
        }
    }
}
