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
					PojisteniId = model.Id,
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
			var platnostList = new SelectList(Enum.GetValues(typeof(DelkaPojisteniValues)));
			ViewBag.PlatnostList = platnostList;
			ViewData["DelkaPojisteni"] = platnostList.First().Value;
			var pojistnaSmlouva = await _context.PojistneSmlouvy
				.Include(ps => ps.Pojisteni)
				.FirstOrDefaultAsync(ps => ps.Id == id);
			//vytvoříme viewModel
			var pojistnaSmlouvaVM = new SjednaniPojisteniViewModel
			{
				Id = pojistnaSmlouva.Id,
				DelkaPojisteni = pojistnaSmlouva.DelkaPojisteni,
				Nazev = pojistnaSmlouva.Pojisteni.Nazev,
				Expirace = pojistnaSmlouva.Expirace
			};

			if (pojistnaSmlouva == null)
			{
				return NotFound();
			}
			//Console.WriteLine($"Edit: Id z URL: {id}, Id modelu: {pojistnaSmlouva.Id}");
			Console.WriteLine("Id pojistné smlouvy: "+pojistnaSmlouvaVM.Id);
			Console.WriteLine("Název pojistné smlouvy: "+pojistnaSmlouvaVM.Nazev);
			return View(pojistnaSmlouvaVM);
		}

		// POST: PojistneSmlouvy/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,DelkaPojisteni")] SjednaniPojisteniViewModel model) //prodloužení
		{
			if (id != model.Id)
			{
				//return NotFound();
				return View("NotFound"); //not found 
			}
			ModelState.Remove("Nazev");
			if (ModelState.IsValid)
			{
				try
				{
					Console.WriteLine((int)model.DelkaPojisteni);
					var pojistnaSmlouva = await _context.PojistneSmlouvy
						.Include(ps => ps.Pojisteni)
						.FirstOrDefaultAsync(ps => ps.Id == model.Id);
					pojistnaSmlouva.DelkaPojisteni=model.DelkaPojisteni;
					pojistnaSmlouva.Expirace = pojistnaSmlouva.Expirace.AddDays((int)pojistnaSmlouva.DelkaPojisteni);
					Console.WriteLine(pojistnaSmlouva.Expirace);
					_context.Update(pojistnaSmlouva);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PojistnaSmlouvaExists(model.Id))
					{
						//return View("NotFound");
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			else
			{
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					// Tohle vypíše všechny chyby v konzoli (nebo logu)
					Console.WriteLine(error.ErrorMessage);
				}

				// Zobrazení formuláře s chybami
				Console.WriteLine("Jdu na index");
				return View(model);
				//return View(nameof(Index));
			}


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
