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
    public class UsersController : Controller
    {
        private readonly TourReContext _context;

        public UsersController(TourReContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            return View(await _context.User.ToListAsync());
        }

        // GET: Admin/Users/Details/5
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

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Username,Pass,Email,Phone,Adress,Cmt")] User user)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
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

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Username,Pass,Email,Phone,Adress,Cmt")] User user)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Name = HttpContext.Session.GetString("namead");
            ViewBag.Img = HttpContext.Session.GetString("image");
            ViewBag.Pass = HttpContext.Session.GetString("pass");
            ViewBag.Id = HttpContext.Session.GetString("id");
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
