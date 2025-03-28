using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_full.Models;

namespace Project_full.Data
{

	public class ApplicationDbContext : IdentityDbContext<Osoba>
	{
		//public DbSet<Osoba> Osoby { get; set; }
		public DbSet<Pojisteni> Pojisteni { get; set; }
		public DbSet<PojistnaSmlouva> PojistneSmlouvy { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)	{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{


			// Nastavení cizích klíčů a relací. funguje? nikdo neví...:D
			modelBuilder.Entity<PojistnaSmlouva>()
			.HasOne(p => p.Pojistenec)  // PojistnaSmlouva má jednoho Pojistence
			.WithMany()                  // Osoba může mít více PojistnychSmluv
			.HasForeignKey(p => p.PojistenecId)  // Cizí klíč je PojistenecId
			.OnDelete(DeleteBehavior.SetNull); //  při smazání pojistence se záznam v PojistnaSmlouva nastavi na null

			// Konfigurace pro vztah mezi PojistnaSmlouva a Pojisteni
			modelBuilder.Entity<PojistnaSmlouva>()
				.HasOne(p => p.Pojisteni)   // PojistnaSmlouva má jedno Pojisteni
				.WithMany()                  // Pojisteni může mít více PojistnychSmluv
				.HasForeignKey(p => p.PojisteniId)  // Cizí klíč je PojisteniId
				.OnDelete(DeleteBehavior.Restrict); // při smazání Pojisteni se záznam v PojistnaSmlouva nastavi na null



			base.OnModelCreating(modelBuilder);
		}
	}
}
