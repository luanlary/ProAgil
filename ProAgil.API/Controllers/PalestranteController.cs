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
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepositorio _proAgilRepositorio;

        public PalestranteController(IProAgilRepositorio proAgilRepositorio)
        {
            this._proAgilRepositorio = proAgilRepositorio;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var results = await _proAgilRepositorio.GetAllPalestrantesAscync(true);
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
                var result = await _proAgilRepositorio.GetPalestranteAscyncById(id, true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpGet("getByNome/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {

            try
            {
                var result = await _proAgilRepositorio.GetAllEventoAscyncByTema(nome, true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {

            try
            {
                _proAgilRepositorio.Add(model);

                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {

            try
            {
                var palestrante = await _proAgilRepositorio.GetPalestranteAscyncById(PalestranteId, false);

                if (palestrante == null)
                    return NotFound();

                _proAgilRepositorio.Update(model);

                if (await _proAgilRepositorio.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId, Palestrante model)
        {

            try
            {
                var palestrante = await _proAgilRepositorio.GetPalestranteAscyncById(PalestranteId, false);

                if (palestrante == null)
                    return NotFound();

                _proAgilRepositorio.Delete(palestrante);

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
