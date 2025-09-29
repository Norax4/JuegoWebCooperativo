using Coopera.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Data
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
				.HasKey(jp => new { jp.JugadorId, jp.PartidaId });

			modelBuilder.Entity<JugadorPartida>()
				.HasOne(jp => jp.Jugador)
				.WithMany(j => j.JugadorPartidas)
				.HasForeignKey(jp => jp.JugadorId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JugadorPartida>()
				.HasOne(jp => jp.Partida)
				.WithMany(p => p.PartidaJugadores)
				.HasForeignKey(jp => jp.PartidaId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Partida>()
				.Property(p => p.Dificultad)
				.HasConversion<int>();
		}

		DbSet<Partida> Partidas { get; set; }
		DbSet<Jugador> Jugadores { get; set; }
		DbSet<JugadorPartida> JugadoresPartidas { get; set; }
	}
}
