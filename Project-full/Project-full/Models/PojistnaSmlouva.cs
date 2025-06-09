using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_full.Models
{
	public enum DelkaPojisteniValues
	{
		Měsíční = 30,
		Čtvrtletní = 90,
		Půlroční = 182,
		Roční = 365
	}
	public class PojistnaSmlouva
	{
		/// <summary>
		/// Unikátní identifikátor pojistné smlouvy
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Osoba, která pojištění sjednala a platí ho
		/// </summary>
		//public int? PojistnikId { get; set; }
		//public Osoba Pojistnik { get; set; }

		/// <summary>
		/// Osoba, která požívá výhod pojištění
		/// </summary>
		
		public string? PojistenecId { get; set; }
		[ForeignKey("PojistenecId")]
		public virtual Osoba? Pojistenec { get; set; }

		/// <summary>
		/// Datum, do kterého je pojištění uzavřeno
		/// </summary>
		
		public int? PojisteniId { get; set; }
		[ForeignKey("PojisteniId")]
		public virtual Pojisteni? Pojisteni { get; set; }
		public DelkaPojisteniValues DelkaPojisteni { get; set; }
		public DateTime Expirace { get; set; }
		/// <summary>
		/// Seznam pojistných událostí, které byly hlášeny k sjednané pojistce
		/// mělo by být spíše list objektů PojistnaUdalost, nikoliv stringu....
		/// </summary>

		[Display(Name ="Pojistná Událost")]
		public List<string> PojistnaUdalost { get; private set; } = new List<string>();

	}
}
