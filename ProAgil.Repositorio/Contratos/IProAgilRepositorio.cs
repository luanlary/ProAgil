using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProAgil.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repositorio.Contratos
{
    public interface IProAgilRepositorio
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Eventos
        Task<Evento[]> GetAllEventoAscyncByTema(string tema, bool IncluirPalestrantes);
        Task<Evento[]> GetAllEventoAscync(bool IncluirPalestrantes);
        Task<Evento> GetEventoAscyncById(int EventoId, bool incluirPalestrantes);

        //Palestrantes
        Task<Palestrante[]> GetAllPalestrantesAscyncByName(string nome, bool IncluirEventos);
        Task<Palestrante> GetPalestranteAscyncById(int PalestranteId, bool IncluirEventos);
        Task<Palestrante[]> GetAllPalestrantesAscync(bool IncluirEventos);
    }
}
