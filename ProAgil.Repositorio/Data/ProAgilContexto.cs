using Microsoft.EntityFrameworkCore;
using ProAgil.Dominio.Modelo;

namespace ProAgil.Repositorio.Data
{
    public class ProAgilContexto: DbContext
    {
        public ProAgilContexto(DbContextOptions<ProAgilContexto> options): base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new {PE.EventoId, PE.PalestranteId});
        }       
    }
}