using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_full.Models;

namespace Project_full.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class UserManagementController : Controller
	{
		private readonly UserManager<Osoba> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserManagementController(UserManager<Osoba> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public ActionResult Index()
		{
			var users = _userManager.Users;
			return View(users);

		}

		// GET: HomeController1/Details/5
		public async Task<ActionResult> Details(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return View("NotFound");
			}

			return View(user);
		}



		// GET: /Edit/5
		public async Task<ActionResult> Edit(string id)
		{
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
			public async Task<ActionResult> Edit(string id, [Bind("Id,Jmeno,Prijmeni,Email,DatumNarozeni,TelefonniCislo,Bonus")] OsobaEditViewModel OsobaEditVM)
			{
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

		// GET: HomeController1/Delete/5
		public ActionResult Delete(string id)
		{
			var user = _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// POST: HomeController1/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//	try
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return View("NotFound");
			}
			try
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine("Chyba při mazání: " + ex.Message);
			}
			return View(user);
		}
	}
}
