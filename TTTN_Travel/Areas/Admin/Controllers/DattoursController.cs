using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTTN_Travel.Models;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DattoursController : Controller
    {
        private readonly TourReContext _context;

        public DattoursController(TourReContext context)
        {
            _context = context;
        }

        // GET: Admin/Dattours
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            return View(await _context.Dattour.ToListAsync());
        }

        // GET: Admin/Dattours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dattour = await _context.Dattour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dattour == null)
            {
                return NotFound();
            }

            return View(dattour);
        }

        // GET: Admin/Dattours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dattours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hoten,Tentuor,Sdt,Dc,Email,Date,Songuoi,Treem,Ghichu,Thanhtien,Id")] Dattour dattour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dattour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dattour);
        }

        // GET: Admin/Dattours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dattour = await _context.Dattour.FindAsync(id);
            if (dattour == null)
            {
                return NotFound();
            }
            return View(dattour);
        }

        // POST: Admin/Dattours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Hoten,Tentuor,Sdt,Dc,Email,Date,Songuoi,Treem,Ghichu,Thanhtien,Trangthai,Id")] Dattour dattour)
        {
            if (id != dattour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dattour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DattourExists(dattour.Id))
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
            return View(dattour);
        }

        // GET: Admin/Dattours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dattour = await _context.Dattour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dattour == null)
            {
                return NotFound();
            }

            return View(dattour);
        }

        // POST: Admin/Dattours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dattour = await _context.Dattour.FindAsync(id);
            _context.Dattour.Remove(dattour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DattourExists(int id)
        {
            return _context.Dattour.Any(e => e.Id == id);
        }
    }
}
