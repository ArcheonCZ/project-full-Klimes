using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_full.Models
{
	public class SjednaniPojisteniViewModel
	{
		public int Id { get; set; }
		public DelkaPojisteniValues DelkaPojisteni { get; set; }
		
		public  string Nazev { get; set; }
		public DateTime Expirace { get; set; }
	}
}
