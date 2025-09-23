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

			modelBuilder.Entity<Partida>()
				.HasMany(jugadores => jugadores.Jugadores)
				.WithOne(jugador => jugador.Partida)
				.HasForeignKey(jugador => jugador.PartidaId);

			modelBuilder.Entity<Partida>()
				.HasMany(recursos => recursos.Recursos)
				.WithOne(recurso => recurso.Partida)
				.HasForeignKey(recurso=> recurso.PartidaId);

			modelBuilder.Entity<JugadorRecurso>()
				.HasKey(jr => new { jr.JugadorId, jr.RecursoId });
			
			modelBuilder.Entity<JugadorRecurso>()
				.HasOne(jr => jr.Jugador)
				.WithMany(j => j.JugadorRecursos)
				.HasForeignKey(jr => jr.JugadorId);

			modelBuilder.Entity<JugadorRecurso>()
				.HasOne(jr => jr.Recurso)
				.WithMany(r => r.JugadorRecursos)
				.HasForeignKey(jr => jr.RecursoId);

			modelBuilder.Entity<Partida>()
				.ToTable("Partidas");
			modelBuilder.Entity<Recurso>()
				.ToTable("Recursos");
			modelBuilder.Entity<Jugador>()
				.ToTable("Jugadores");
			modelBuilder.Entity<JugadorRecurso>()
				.ToTable("JugadorRecursos");
			}

		DbSet<Partida> Partidas { get; set; }
		DbSet<Jugador> Jugadores { get; set; }
		DbSet<JugadorRecurso> JugadoresRecursos { get; set; }
		DbSet<Recurso> Recursos { get; set; }
		}
	}