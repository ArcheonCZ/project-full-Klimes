using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_full.Models

{
	public class Osoba
	{
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Jméno pojištěnce
		/// </summary>
		[Required]
		public string Jmeno { get; set; } = "";
		/// <summary>
		/// Příjmení pojištěnce
		/// </summary>
		[Required]
		public string Prijmeni { get; set; } = "";
		/// <summary>
		/// Datum narození pojištěnce
		/// </summary>
		public DateTime DatumNarozeni { get; set; }
		/// <summary>
		/// Věk pojištěnce - automaticky dogenerovaný
		/// </summary>
		[NotMapped]
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
		public string TelefonniCislo { get; set; } = "";

		/// <summary>
		/// 	Bonus (plusové hodnoty) / Malus (minusové hodnoty)
		/// </summary>
		public int Bonus { get; set; }

		/// <summary>
		/// Cizí klíč na tabulku AspNetUsers
		/// </summary>
		[Required]
		public string UserId { get; set; }

		/// <summary>
		/// Seznam uzavřených pojistných smluv
		/// </summary>
		public List<PojistnaSmlouva> SeznamPojisteni = new List<PojistnaSmlouva>();


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


