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
		//public int? PojistnikId { get; set; }
		//public Osoba Pojistnik { get; set; }

		/// <summary>
		/// Osoba, která požívá výhod pojištění
		/// </summary>
		public int? PojistenecId { get; set; }
		//public Osoba Pojistenec { get; set; }

		/// <summary>
		/// Datum, do kteréhj e pojištění uzavřeno
		/// </summary>
		public int? PojisteniId { get; set; }
		//public Pojisteni Pojisteni { get; set; }
		public DateTime Expirace { get; private set; }
		/// <summary>
		/// Seznam pojistných událostí, které byly hlášeny k sjednané pojistce
		/// mělo by být spíše list objektů PojistnaUdalost, nikoliv stringu....
		/// </summary>

		/// Zde by se hodila vytvořit třída PojistnaUdalost a ne primitivní string, ale pro jednoduchost takto
		public List<string> PojistnaUdalost { get; private set; } = new List<string>();

	}
}
