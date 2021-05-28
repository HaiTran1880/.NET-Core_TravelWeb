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
    public class TintucsController : Controller
    {
        private readonly TourReContext _context;

        public TintucsController(TourReContext context)
        {

            _context = context;
        }

        // GET: Admin/Tintucs
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");


            return View(await _context.Tintuc.ToListAsync());
        }

        // GET: Admin/Tintucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintuc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // GET: Admin/Tintucs/Create
        public IActionResult Create()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            return View();
        }

        // POST: Admin/Tintucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tentintuc,Anhtintuc,Tomtat,Chitiet,Date,ImageFile")] Tintuc tintuc)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (ModelState.IsValid)
            {
                UploadFiles.ImgURL(tintuc.ImageFile);
                tintuc.Chitiet = Request.Form["Chitiet"].ToString();
                tintuc.Anhtintuc = tintuc.ImageFile.FileName;
                _context.Add(tintuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tintuc);
        }

        // GET: Admin/Tintucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (id == null)
            {
                return NotFound();
            }

            var tintuc = await _context.Tintuc.FindAsync(id);
            if (tintuc == null)
            {
                return NotFound();
            }
            return View(tintuc);
        }

        // POST: Admin/Tintucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tentintuc,Anhtintuc,Tomtat,Chitiet,Date,ImageFile")] Tintuc tintuc)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (id != tintuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tintuc.Anhtintuc = tintuc.ImageFile.FileName;
                    _context.Update(tintuc);
                    UploadFiles.ImgURL(tintuc.ImageFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TintucExists(tintuc.Id))
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
            return View(tintuc);
        }

        // GET: Admin/Tintucs/Delete/5
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

            var tintuc = await _context.Tintuc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tintuc == null)
            {
                return NotFound();
            }

            return View(tintuc);
        }

        // POST: Admin/Tintucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            var tintuc = await _context.Tintuc.FindAsync(id);
            _context.Tintuc.Remove(tintuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TintucExists(int id)
        {
            return _context.Tintuc.Any(e => e.Id == id);
        }
    }
}
