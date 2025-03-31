using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_full.Data;
using Project_full.Models;

namespace Project_full.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
			_context = context;
		}
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult SeznamPojisteni()
        {
			if (!_context.Pojisteni.Any())  // Kontrola, zda tabulka není prázdná
			{
				var pojisteniList = new List<Pojisteni>
			{
				new Pojisteni { Nazev = "Pojištìní zdraví", Cena = 1000 , PojistnaCastka=10000000},
				new Pojisteni { Nazev = "Pojištìní auta", Cena = 5000, PojistnaCastka=5000000 },
				new Pojisteni { Nazev = "Pojištìní domácnosti", Cena = 2000, PojistnaCastka=2000000 }
			};

				_context.Pojisteni.AddRange(pojisteniList);
				_context.SaveChanges();
			}

			// Naèteme všechna pojištìní z databáze
			var pojisteni = _context.Pojisteni.ToList();
			return View(pojisteni);
			
		}

		//public IActionResult DetailPojisteni(int id)
		//{
		//	var pojisteni = _context.Pojisteni.FirstOrDefault(p => p.Id == id);  // Naète detail pojištìní podle ID
		//	if (pojisteni == null)
		//	{
		//		return NotFound();  // Pokud není pojištìní nalezeno, vrátí 404
		//	}

		//	return View(pojisteni);
		//}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
