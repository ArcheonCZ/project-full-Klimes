namespace Project_full.Models
{
	public class Pojisteni
	{
		/// <summary>
		/// Primary key
		/// </summary>
		public int Id { get;  set; }
		/// <summary>
		/// Název pojištění
		/// </summary>
		public string Nazev { get;  set; } = "";
		/// <summary>
		/// Cena pojištění
		/// </summary>
		public float Cena { get;  set; }
		/// <summary>
		/// Údaj deklaruje, maximální částku, kterou pojišťovna vyplatí při pojistné události
		/// </summary>
		public float PojistnaCastka { get;  set; }

	}
}
