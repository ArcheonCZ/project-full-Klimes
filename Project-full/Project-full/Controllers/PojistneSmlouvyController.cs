using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_full.Data;
using Project_full.Models;

namespace Project_full.Controllers
{
    public class PojistneSmlouvyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PojistneSmlouvyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PojistneSmlouvy
        public async Task<IActionResult> Index()
        {
            return View(await _context.PojistneSmlouvy.ToListAsync());
        }

        // GET: PojistneSmlouvy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojistnaSmlouva = await _context.PojistneSmlouvy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojistnaSmlouva == null)
            {
                return NotFound();
            }

            return View(pojistnaSmlouva);
        }

        // GET: PojistneSmlouvy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PojistneSmlouvy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PojistenecId,PojisteniId,Expirace,PojistnaUdalost")] PojistnaSmlouva pojistnaSmlouva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pojistnaSmlouva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pojistnaSmlouva);
        }

        // GET: PojistneSmlouvy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojistnaSmlouva = await _context.PojistneSmlouvy.FindAsync(id);
            if (pojistnaSmlouva == null)
            {
                return NotFound();
            }
            return View(pojistnaSmlouva);
        }

        // POST: PojistneSmlouvy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PojistenecId,PojisteniId,Expirace,PojistnaUdalost")] PojistnaSmlouva pojistnaSmlouva)
        {
            if (id != pojistnaSmlouva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pojistnaSmlouva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojistnaSmlouvaExists(pojistnaSmlouva.Id))
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
            return View(pojistnaSmlouva);
        }

        // GET: PojistneSmlouvy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojistnaSmlouva = await _context.PojistneSmlouvy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojistnaSmlouva == null)
            {
                return NotFound();
            }

            return View(pojistnaSmlouva);
        }

        // POST: PojistneSmlouvy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pojistnaSmlouva = await _context.PojistneSmlouvy.FindAsync(id);
            if (pojistnaSmlouva != null)
            {
                _context.PojistneSmlouvy.Remove(pojistnaSmlouva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PojistnaSmlouvaExists(int id)
        {
            return _context.PojistneSmlouvy.Any(e => e.Id == id);
        }
    }
}
