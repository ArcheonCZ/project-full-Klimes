using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_full.Models
{
	public class SjednaniPojisteniViewModel
	{
		public int PojisteniId { get; set; }
		public int PojistnaSmlouvaId { get; set; }
		public DelkaPojisteniValues DelkaPojisteni { get; set; }
		
		public  string Nazev { get; set; }
		public DateTime Expirace { get; set; }
		
		public SelectList? PojisteniOptions { get; set; }  // Pojisteni SelectList
		
		public SelectList? DelkaPojisteniOptions { get; set; } // DelkaPojisteni SelectList
	}
}
