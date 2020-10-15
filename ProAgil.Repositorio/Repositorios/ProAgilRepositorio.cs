using Microsoft.EntityFrameworkCore;
using ProAgil.Dominio.Modelo;
using ProAgil.Repositorio.Contratos;
using ProAgil.Repositorio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repositorio.Repositorios
{
    public class ProAgilRepositorio : IProAgilRepositorio
    {
        public ProAgilContexto _contexto { get; }

        public ProAgilRepositorio(ProAgilContexto contexto)
        {
            this._contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            this._contexto.Add(entity);
        }
        
        public void Update<T>(T entity) where T : class
        {
            this._contexto.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._contexto.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventoAscync(bool IncluirPalestrantes = false)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (IncluirPalestrantes)
                query = query.Include(Pe => Pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAscyncByTema(string tema, bool IncluirPalestrantes = false)
        {
            IQueryable<Evento> query = _contexto.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if (IncluirPalestrantes)
                query = query.Include(Pe => Pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAscyncById(int EventoId, bool IncluirPalestrantes = false)
        {
            IQueryable<Evento> query = _contexto.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if (IncluirPalestrantes)
                query = query.Include(Pe => Pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento)
                .Where(c => c.Id == EventoId);
            return await query.FirstOrDefaultAsync();

        }

        public async Task<Palestrante> GetPalestranteAscyncById(int PalestranteId, bool IncluirEventos = false)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes            
            .Include(c => c.RedesSociais);

            if (IncluirEventos)
                query = query.Include(Pe => Pe.PalestranteEventos)
                        .ThenInclude(e => e.Evento);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
                .Where(c => c.Id == PalestranteId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAscyncByName(string nome, bool IncluirEventos = false)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
                .Include(c => c.RedesSociais);
            if (IncluirEventos)
                query = query.Include(Pe => Pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            query = query.AsNoTracking().OrderBy(p => p.Nome)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAscync(bool IncluirEventos = false)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
                .Include(c => c.RedesSociais);
            if (IncluirEventos)
                query = query.Include(Pe => Pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            query = query.AsNoTracking().OrderBy(p => p.Nome);
            return await query.ToArrayAsync();
        }


    }
}
