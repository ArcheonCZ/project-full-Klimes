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
		public async Task<ActionResult> Edit(string id, [Bind("Jmeno,Prijmeni,Email,DatumNarozeni,TelefonniCislo,Bonus")] Osoba osoba)
		{
			Console.WriteLine("string Id: "+id);
			Console.WriteLine("Id uživatele: "+osoba.Id);
			if (!id.Contains(osoba.Id))
			{
				Console.WriteLine("Uživatel nenalezen - nemohu  editovat 2");
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _userManager.UpdateAsync(osoba);
					//await _userManager.SaveChangesAsync(); //Entity Framework Core se o uložení postará samo
				}
				catch (DbUpdateConcurrencyException)
				{
				}
				//try
				//{
				//	return RedirectToAction(nameof(Index));
				//}
				//catch
				//{
				//return View();
				//}
				return RedirectToAction(nameof(Index));
			}
			return View(osoba);
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
