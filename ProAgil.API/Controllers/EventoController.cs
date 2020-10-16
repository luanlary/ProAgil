using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Dominio.Modelo;
using ProAgil.Repositorio.Contratos;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepositorio _proAgilRepositorio;

        public EventoController(IProAgilRepositorio proAgilRepositorio)
        {
            this._proAgilRepositorio = proAgilRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var results = await _proAgilRepositorio.GetAllEventoAscync(true);
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
                var result = await _proAgilRepositorio.GetEventoAscyncById(id, true);
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
                var result = await _proAgilRepositorio.GetAllEventoAscyncByTema(tema, true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {

            try
            {
                _proAgilRepositorio.Add(model);
                
                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
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
        public async Task<IActionResult> Put(int id, Evento model)
        {

            try
            {
                var evento = await _proAgilRepositorio.GetEventoAscyncById(id, false);

                if (evento == null)
                    return NotFound();

                _proAgilRepositorio.Update(model);

                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
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
