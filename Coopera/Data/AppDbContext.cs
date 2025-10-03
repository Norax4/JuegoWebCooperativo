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
				.HasKey(jp => jp.Id);

			modelBuilder.Entity<JugadorPartida>()
				.HasIndex(jp => new { jp.JugadorId, jp.PartidaId })
				.IsUnique();

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

		public DbSet<Partida> Partidas { get; set; }
		public DbSet<Jugador> Jugadores { get; set; }
		public DbSet<JugadorPartida> JugadoresPartidas { get; set; }
	}
}
