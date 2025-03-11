using Microsoft.AspNetCore.Http;

namespace Project_full.Models
{
	public class PojistnaSmlouva
	{
		/// <summary>
		/// Unikátní identifikátor pojistné smlouvy
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Osoba, která pojištění sjednala a platí ho
		/// </summary>
		public Osoba Pojistnik { get; private set; }
		/// <summary>
		/// Osoba, která požívá výhod pojištění
		/// </summary>
		public Osoba Pojistenec { get; private set; }

		/// <summary>
		/// Datum, do kteréhj e pojištění uzavřeno
		/// </summary>
		public DateTime Expirace { get; private set; }
		/// <summary>
		/// Seznam pojistných událostí, které byly hlášeny k sjednané pojistce
		/// mělo by být spíše list objektů PojistnaUdalost, nikoliv stringu....
		/// </summary>
		public List<string> PojistnaUdalost { get; private set; } = new List<string>(); 

	}
}
