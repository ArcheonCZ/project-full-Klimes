using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_full.Models;

namespace Project_full.Data
{

	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Osoba> Osoby { get; set; }
		public DbSet<Pojisteni> Pojisteni { get; set; }
		public DbSet<PojistnaSmlouva> PojistneSmlouvy { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)	{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pojisteni>()
	   .HasKey(p => p.Id);  // Pojisteni.Id je primární klíč

			// Nastavení cizích klíčů a relací. funguje? nikdo neví...:D
			modelBuilder.Entity<PojistnaSmlouva>()
				.HasOne<Osoba>()
				.WithOne()
				.HasForeignKey<PojistnaSmlouva>(ps => ps.PojistnikId);

			modelBuilder.Entity<PojistnaSmlouva>()
				.HasOne<Osoba>()
				.WithOne()
				.HasForeignKey<PojistnaSmlouva>(ps => ps.PojistenecId);

			modelBuilder.Entity<PojistnaSmlouva>()
				.HasOne<Pojisteni>()
				.WithOne()
				.HasForeignKey<PojistnaSmlouva>(ps => ps.PojisteniId);
		}
	}
}
