using Microsoft.EntityFrameworkCore;
using ProAgil.Dominio.Modelo;

namespace ProAgil.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        
    }
}