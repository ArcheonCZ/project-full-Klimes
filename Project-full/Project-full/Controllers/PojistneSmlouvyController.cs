using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<Osoba> _userManager;

		public PojistneSmlouvyController(ApplicationDbContext context, UserManager<Osoba> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: PojistneSmlouvy
		public async Task<IActionResult> Index()
		{
			var pojistneSmlouvy = await _context.PojistneSmlouvy
		// .Include(ps => ps.Pojistenec)  // Načítáme související osobu (pojištěného)
		.Include(ps => ps.Pojisteni)   // Načítáme související pojištění
		.ToListAsync();

			return View(pojistneSmlouvy);

			//return View(await _context.PojistneSmlouvy.ToListAsync());
		}

		// GET: PojistneSmlouvy/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var pojistnaSmlouva = await _context.PojistneSmlouvy
				.Include(ps => ps.Pojisteni)
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
			var pojisteniList = _context.Pojisteni.ToList();
			var model = new SjednaniPojisteniViewModel();
			ViewBag.PojisteniList = new SelectList(pojisteniList, "Id", "Nazev", "Cena");
			ViewBag.PlatnostList = new SelectList(Enum.GetValues(typeof(DelkaPojisteniValues)));
			return View(model);
		}

		// POST: PojistneSmlouvy/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SjednaniPojisteniViewModel model)
		{
			if (ModelState.IsValid)
			{
				var novaPojistnaSmlouva = new PojistnaSmlouva
				{
					PojistenecId = _userManager.GetUserId(User),//osoba přihlášená
					PojisteniId = model.PojisteniId,
					DelkaPojisteni = model.DelkaPojisteni,
					Expirace = DateTime.Now.AddDays((int)model.DelkaPojisteni)
				};
				_context.PojistneSmlouvy.Add(novaPojistnaSmlouva);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		// GET: PojistneSmlouvy/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			ViewBag.PlatnostList = new SelectList(Enum.GetValues(typeof(DelkaPojisteniValues)));
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,DelkaPojisteni")] PojistnaSmlouva pojistnaSmlouva) //prodloužení
		{
			if (id != pojistnaSmlouva.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					pojistnaSmlouva.Expirace = pojistnaSmlouva.Expirace.AddDays((int)pojistnaSmlouva.DelkaPojisteni);
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
				.Include(ps => ps.Pojisteni)
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
