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

namespace TTTN_Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddminsController : Controller
    {
        private readonly TourReContext _context;

        public AddminsController(TourReContext context)
        {
            _context = context;
        }

        // GET: Admin/Addmins
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            return View(await _context.Admin.ToListAsync());
        }

        // GET: Admin/Addmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var addmin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Idad == id);
            if (addmin == null)
            {
                return NotFound();
            }

            return View(addmin);
        }

        // GET: Admin/Addmins/Create
        public IActionResult Create()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            return View();
        }

        // POST: Admin/Addmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idad,Username,Passwork,ImageFile")] Addmin addmin)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (ModelState.IsValid)
            {
                UploadFiles.ImgURL(addmin.ImageFile);
                addmin.Image = addmin.ImageFile.FileName;
                _context.Add(addmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addmin);
        }

        // GET: Admin/Addmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var addmin = await _context.Admin.FindAsync(id);
            if (addmin == null)
            {
                return NotFound();
            }
            return View(addmin);
        }

        // POST: Admin/Addmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idad,Username,Passwork,Image,ImageFile")] Addmin addmin)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id != addmin.Idad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     addmin.Image = addmin.ImageFile.FileName;
                    _context.Update(addmin);
                    UploadFiles.ImgURL(addmin.ImageFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddminExists(addmin.Idad))
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
            return View(addmin);
        }

        // GET: Admin/Addmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            if (id == null)
            {
                return NotFound();
            }

            var addmin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Idad == id);
            if (addmin == null)
            {
                return NotFound();
            }

            return View(addmin);
        }

        // POST: Admin/Addmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            var addmin = await _context.Admin.FindAsync(id);
            //UploadFiles.RemoveImgURL("/Upload/a/" + addmin.Image);
            _context.Admin.Remove(addmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddminExists(int id)
        {
            return _context.Admin.Any(e => e.Idad == id);
        }
    }
}
