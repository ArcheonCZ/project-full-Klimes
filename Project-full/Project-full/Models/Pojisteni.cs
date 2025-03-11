namespace Project_full.Models
{
	public class Pojisteni
	{
		/// <summary>
		/// Název pojištění
		/// </summary>
		public string Nazev { get; private set; } = "";
		/// <summary>
		/// Cena pojištění
		/// </summary>
		public float Cena { get; private set; }
		/// <summary>
		/// Údaj deklaruje, maximální částku, kterou pojišťovna vyplatí při pojistné události
		/// </summary>
		public float PojistnaCastka { get; private set; }

	}
}
