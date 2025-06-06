﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace Project_full.Models

{
	public class Osoba: IdentityUser
	{
		/// <summary>
		/// Primary key
		/// </summary>
		//[Key]
		//public string Id { get; set; }  ///=> dědím z IdentityUSer, který má vlastní ID

		/// <summary>
		/// Jméno pojištěnce
		/// </summary>
		[Required, Display(Name = "Jméno")]
		public string Jmeno { get; set; } = "";
		/// <summary>
		/// Příjmení pojištěnce
		/// </summary>
		[Required, Display(Name = "Příjmení")]
		public string Prijmeni { get; set; } = "";
		/// <summary>
		/// Datum narození pojištěnce
		/// </summary>
		[Required, Display(Name = "Datum narození")]
		public DateTime DatumNarozeni { get; set; }
		/// <summary>
		/// Věk pojištěnce - automaticky dogenerovaný
		/// </summary>
		[NotMapped, Display(Name = "Věk")]
		public int Vek
		{
			get
			{
				DateTime dnes = DateTime.Now;
				int vek = dnes.Year - DatumNarozeni.Year;
				if (DatumNarozeni.Date > dnes.AddYears(-vek).Date)
				{
					vek--;
				}
				return vek;
			}
		}
		/// <summary>
		/// Telefonní číslo pojištěnce
		/// </summary>
		
		[Required, Phone, Display(Name = "Telefonní číslo")]
		public string TelefonniCislo { get; set; } = "";

		/// <summary>
		/// 	Bonus (plusové hodnoty) / Malus (minusové hodnoty)
		/// </summary>
		public int Bonus { get; set; }

		/// <summary>
		/// Cizí klíč na tabulku AspNetUsers
		/// </summary>

		//public string UserId { get; set; }

		/// <summary>
		/// Seznam uzavřených pojistných smluv
		/// </summary>
		[Display(Name ="Seznam pojištění")]
		public  List<PojistnaSmlouva> SeznamPojisteni = new List<PojistnaSmlouva>();

		public Osoba UpravUdaje (OsobaEditViewModel osobaEditVM)
		{
			PropertyInfo[] osobaProperties = typeof(Osoba).GetProperties();
			PropertyInfo[] osobaVMProperties = typeof(OsobaEditViewModel).GetProperties();
			foreach (PropertyInfo prop in osobaProperties)
			{
				if (prop.CanWrite)
				{
					//pokud sedí jméno a datový typ
					var vmProp = osobaVMProperties.FirstOrDefault(p => p.Name == prop.Name && p.PropertyType == prop.PropertyType);
					if (vmProp != null)
					{
						// Získáme hodnotu z ViewModelu
						var novaHodnota = vmProp.GetValue(osobaEditVM);
						// Nastavíme do aktuální instance Osoba
						prop.SetValue(this, novaHodnota);
					}
				}
				else
					continue;
			}
			return this;
		}




		/// <summary>
		/// Pomocná metoda pro změnu skloňování slova roky/let dle věku
		/// </summary>
		/// <param name="Vek"></param>
		/// <returns>vrací string s korektním skloňováním slova roky/let</returns>
		//public string KorektniSklonovaniVeku(int Vek)
		//{
		//	string sklonovaniVeku = "";
		//	switch (Vek)
		//	{
		//		case 1:
		//			sklonovaniVeku = "rok";
		//			break;
		//		case int c when (c > 1 && c < 5): //pokud je věk mezi 2-4 roky, vypíše se "roky" - nutno otestovat;
		//			sklonovaniVeku = "roky";
		//			break;
		//		default:
		//			sklonovaniVeku = "let";
		//			break;
		//	}
		//	return $"{Vek} {sklonovaniVeku}";

		//}

		//public override string ToString()
		//{
		//	telefonní číslo by šlo rozepsat dle trojčíslí pro lepší čitelnost pomocnou metodou podobně jako KorektniSklonovaniVeku()
		//	return $"{Jmeno} {Prijmeni}, {KorektniSklonovaniVeku(Vek)}, Telefonní číslo: {TelefonniCislo} ";
		//}
	}
}


