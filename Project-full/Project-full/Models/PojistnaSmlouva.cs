using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_full.Models
{
	public enum DelkaPojisteni
	{
		Měsíční = 30,
		Čtvrtletní = 90,
		Půlroční = 180,
		Roční = 360
	}
	public class PojistnaSmlouva
	{
		/// <summary>
		/// Unikátní identifikátor pojistné smlouvy
		/// </summary>
		[Key]
		public int Id { get; private set; }
		/// <summary>
		/// Osoba, která pojištění sjednala a platí ho
		/// </summary>
		//public int? PojistnikId { get; set; }
		//public Osoba Pojistnik { get; set; }

		/// <summary>
		/// Osoba, která požívá výhod pojištění
		/// </summary>
		
		public string? PojistenecId { get; set; }
		public virtual Osoba Pojistenec { get; set; }

		/// <summary>
		/// Datum, do kterého je pojištění uzavřeno
		/// </summary>
		public int? PojisteniId { get; set; }
		public virtual Pojisteni Pojisteni { get; set; }
		public DelkaPojisteni DelkaPojisteni { get; set; }
		public DateTime Expirace { get; set; }
		/// <summary>
		/// Seznam pojistných událostí, které byly hlášeny k sjednané pojistce
		/// mělo by být spíše list objektů PojistnaUdalost, nikoliv stringu....
		/// </summary>

		/// Zde by se hodila vytvořit třída PojistnaUdalost a ne primitivní string, ale pro jednoduchost takto
		public List<string> PojistnaUdalost { get; private set; } = new List<string>();

	}
}
