using Coopera.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Datos
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<JugadorPartida>()
				.HasKey(jr => new { jr.JugadorId, jr.PartidaId });

			modelBuilder.Entity<JugadorPartida>()
				.HasOne(jp => jp.Jugador)
				.WithMany(j => j.JugadorPartidas)
				.HasForeignKey(jp => jp.JugadorId);

			modelBuilder.Entity<Partida>()
				.ToTable("Partidas");
			modelBuilder.Entity<Jugador>()
				.ToTable("Jugadores");
			modelBuilder.Entity<JugadorPartida>()
				.ToTable("JugadorRecursos");
		}

		DbSet<Partida> Partidas { get; set; }
		DbSet<Jugador> Jugadores { get; set; }
		DbSet<JugadorPartida> JugadoresPartidas { get; set; }
	}
}