using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using ProAgil.Dominio.Modelo;
using ProAgil.Repositorio.Contratos;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepositorio _proAgilRepositorio;
        private readonly IMapper _mapper;

        public EventoController(IProAgilRepositorio proAgilRepositorio, IMapper mapper)
        {
            this._proAgilRepositorio = proAgilRepositorio;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var eventos = await _proAgilRepositorio.GetAllEventoAscync(true);
                var results = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                var evento = await _proAgilRepositorio.GetEventoAscyncById(id, true);
                var result = _mapper.Map<EventoDto>(evento);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {

            try
            {

                var evento = await _proAgilRepositorio.GetAllEventoAscyncByTema(tema, true);
                var result = _mapper.Map<EventoDto[]>(evento);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {

            try
            {
                var evento = _mapper.Map<Evento>(model);

                _proAgilRepositorio.Add(evento);
                
                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<Evento>(evento));
                }
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {

            try
            {
                var evento = await _proAgilRepositorio.GetEventoAscyncById(id, false);

                if (evento == null)
                    return NotFound();

                _mapper.Map(model, evento);

                _proAgilRepositorio.Update(evento);

                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map(model, evento));
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }
        
        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var evento = await _proAgilRepositorio.GetEventoAscyncById(id, false);

                if (evento == null)
                    return NotFound();

                _proAgilRepositorio.Delete(evento);

                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }
    }
}
