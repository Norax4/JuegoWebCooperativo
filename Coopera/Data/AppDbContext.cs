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
            modelBuilder.Entity<Partida>().ToTable("Partidas");
            modelBuilder.Entity<Jugador>().ToTable("Jugadores");
            modelBuilder.Entity<JugadorPartida>().ToTable("Jugadores_Partidas");
            modelBuilder.Entity<Recurso>().ToTable("Recursos");
            modelBuilder.Entity<RegistroRecursos>().ToTable("Registros_Recursos");

            modelBuilder.Entity<Partida>()
                .HasMany(jp => jp.JugadoresPartidas)
                .WithOne(p => p.Partida)
                .HasForeignKey(p => p.PartidaID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Partida>()
                .HasMany(pr => pr.RegistroIngresos)
                .WithOne(p => p.Partida)
                .HasForeignKey(p => p.PartidaID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Jugador>()
                .HasMany(jp => jp.PartidasJugadas)
                .WithOne(j => j.Jugador)
                .HasForeignKey(j => j.JugadorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Recurso>()
                .HasMany(rr => rr.Registros)
                .WithOne(r => r.Recurso)
                .HasForeignKey(r => r.RecursoID)
                .OnDelete(DeleteBehavior.Cascade);
        }

        DbSet<Partida> Partidas { get; set; }
        DbSet<Jugador> Jugadores { get; set; }
        DbSet<JugadorPartida> JugadoresPartidas { get; set; }
        DbSet<Recurso> Recursos { get; set; }
        DbSet<RegistroRecursos> RegistrosRecursos { get; set; }
    }
}
