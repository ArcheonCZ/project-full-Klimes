using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_full.Data;
using Project_full.Models;

namespace Project_full.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class PojisteniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PojisteniController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: Pojisteni
		[AllowAnonymous]
		public async Task<IActionResult> Index()
        {
            return View(await _context.Pojisteni.ToListAsync());
        }

		// GET: Pojisteni/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojisteni = await _context.Pojisteni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojisteni == null)
            {
                return NotFound();
            }

            return View(pojisteni);
        }

        // GET: Pojisteni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pojisteni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazev,Cena,PojistnaCastka")] Pojisteni pojisteni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pojisteni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pojisteni);
        }

        // GET: Pojisteni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pojisteni = await _context.Pojisteni.FindAsync(id);
            if (pojisteni == null)
            {
                return NotFound();
            }
            return View(pojisteni);
        }

        // POST: Pojisteni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazev,Cena,PojistnaCastka")] Pojisteni pojisteni)
        {
            if (id != pojisteni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pojisteni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojisteniExists(pojisteni.Id))
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
            return View(pojisteni);
        }

        // GET: Pojisteni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
    //        var pojistneSmlouvy = await _context.PojistneSmlouvy
				//.FirstOrDefaultAsync(ps => ps.Id == id); 

            var pojisteni = await _context.Pojisteni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojisteni == null)
            {
                return NotFound();
            }

            return View(pojisteni);
        }

        // POST: Pojisteni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pojisteni = await _context.Pojisteni.FindAsync(id);
			if (pojisteni == null)
			{
				return View("NotFound");
			}
			try
			{
				_context.Pojisteni.Remove(pojisteni);
				await _context.SaveChangesAsync();

			}

			catch (DbUpdateException ex)
			{
				Console.WriteLine("Chyba při mazání: " + ex.Message);
				TempData["ErrorMessage"] = "Nelze smazat pojištění, protože na něj existují sjednané pojistné smlouvy.";
				Console.WriteLine("zachycena žádaná -chybová- hláška při mazání");
			}
			return RedirectToAction(nameof(Index));
        }

        private bool PojisteniExists(int id)
        {
            return _context.Pojisteni.Any(e => e.Id == id);
        }
    }
}
