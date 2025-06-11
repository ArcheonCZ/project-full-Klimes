using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_full.Data;
using Project_full.Models;
using System.Security.Claims;

namespace Project_full.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class UserManagementController : Controller
	{
		private readonly UserManager<Osoba> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _context;

		public UserManagementController(UserManager<Osoba> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}

		public ActionResult Index()
		{
			var users = _userManager.Users;
			return View(users);

		}

		// GET: HomeController1/Details/5
		[AllowAnonymous, HttpGet]
		public async Task<ActionResult> Details(string id, bool zobrazTlacitkoZpet = true)
		{
			bool jeAdmin = User.IsInRole(UserRoles.Admin);  // zjistí, jestli má uživatel roli Admin
			bool jeVlastnik = id == User.FindFirstValue(ClaimTypes.NameIdentifier); // zda id detailu je stejné jako id přihlášeného uživatele
			Console.WriteLine("jeAdmin: "+ jeAdmin );
			Console.WriteLine("jeVlastnik" + jeVlastnik);
			if (!jeVlastnik && !jeAdmin)
				return Unauthorized();
			//Console.WriteLine("ZobrazTlacitkoZpet v controlleru: "+ zobrazTlacitkoZpet);
			if (zobrazTlacitkoZpet)
			{
				ViewBag.ZobrazTlacitkoZpet = true;
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return View("NotFound");
			}
			List<PojistnaSmlouva>? pojistneSmlouvyUser = _context.PojistneSmlouvy
				.Include(p => p.Pojisteni)   // Načítáme související pojištění
				.Where(p => p.PojistenecId == id)
				.ToList();
			user.SeznamPojisteni = pojistneSmlouvyUser;
			return View(user);
		}



		// GET: /Edit/5
		[AllowAnonymous]
		public async Task<ActionResult> Edit(string id)
		{
			bool jeAdmin = User.IsInRole(UserRoles.Admin);  // zjistí, jestli má uživatel roli Admin
			bool jeVlastnik = id == User.FindFirstValue(ClaimTypes.NameIdentifier); // zda id detailu je stejné jako id přihlášeného uživatele
			//Console.WriteLine("jeAdmin: "+ jeAdmin );
			//Console.WriteLine("jeVlastnik" + jeVlastnik);
			if (!jeVlastnik && !jeAdmin)
				return Unauthorized();

			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				Console.WriteLine("Uživatel nenalezen - nemohu  editovat");
				return NotFound();

			}
			return View(user);
		}


		// POST: /Edit/5
			[HttpPost]
			[ValidateAntiForgeryToken]
			[AllowAnonymous]
			public async Task<ActionResult> Edit(string id, [Bind("Id,Jmeno,Prijmeni,Email,DatumNarozeni,TelefonniCislo,Bonus")] OsobaEditViewModel OsobaEditVM)
			{
			bool jeAdmin = User.IsInRole(UserRoles.Admin);  // zjistí, jestli má uživatel roli Admin
			bool jeVlastnik = id == User.FindFirstValue(ClaimTypes.NameIdentifier); // zda id detailu je stejné jako id přihlášeného uživatele
			Console.WriteLine("jeAdmin: "+ jeAdmin );
			Console.WriteLine("jeVlastnik" + jeVlastnik);
			if (!jeVlastnik && !jeAdmin)
				return Unauthorized();

			Console.WriteLine("string Id: "+id);
				Console.WriteLine("Id uživatele: "+OsobaEditVM.Id);
				if (!id.Contains(OsobaEditVM.Id))
				{
					Console.WriteLine("Uživatelům nesedí id - nemohu  editovat 2");
					return NotFound();
				}

				if (!ModelState.IsValid)
				{
					Console.WriteLine("Model state neni validni!!!");
					return NotFound();
				}
				else
				{
					Console.WriteLine("Edit model state je validní!");
					Osoba? upravenaOsoba = await _userManager.FindByIdAsync(id);
					if (upravenaOsoba == null)
						return NotFound();
					upravenaOsoba.UpravUdaje(OsobaEditVM);
					await _userManager.UpdateAsync(upravenaOsoba);
					//try
					//{
					//	await _userManager.UpdateAsync(osoba);
					////await _userManager.SaveChangesAsync(); //Entity Framework Core se o uložení postará samo
					//return RedirectToAction(nameof(Index));
					//}
					//catch (DbUpdateConcurrencyException)
					//{
					//	throw;
					//}

					return RedirectToAction(nameof(Index));
			}
			}

	
	
	}
}
