using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrhhApi.Models;
using Serilog;

namespace rrhhApi.Controllers
{
    [Route("api/Candidato")]
    [Produces("application/json")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly RRHHContext _context;

        public CandidatoController(RRHHContext context)
        {
            _context = context;
        }

        // GET: api/Candidato
        /// <summary>
        /// Lista los candidatos con sus empleos.
        /// </summary>
        /// <returns>Devuelve un JSON de todos los candidatos con sus empleos</returns>
        /// <response code="200">El listado de candidatos en caso de éxito</response>
        /// <response code="400">Si no existe la entidad Candidatos</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidatos()
        {
            if (_context.Candidatos == null)
            {
                return NotFound();
            }
            Log.Information("Método GET: listado de candidatos con sus empleos");
            return await _context.Candidatos.Include(x => x.Empleos).ToListAsync();
        }

        // GET: api/Candidato/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            if (_context.Candidatos == null)
            {
                return NotFound();
            }
            var candidato = await _context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            Log.Information("Método GET: Obtiene un candidato a través de su id");
            return candidato;
        }


        // PUT: api/Candidato/5
        /// <summary>
        /// Modifica un candidato específico.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="candidato"></param>
        /// <returns>No devuelve contenido</returns>
        /// <response code="204">No devuelve contenido en caso de éxito</response>
        /// <response code="400">Si el id del candidato a actualizar es distinto al candidato pasasdo</response>
        /// <response code="404">Si el id del candidato a actualizar no se encuentra como candidato</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, Candidato candidato)
        {
            if (id != candidato.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Log.Information("Método PUT: actualiza un candidato");
            return NoContent();
        }

        // POST: api/Candidato
        /// <summary>
        /// Crea un nuevo candidato.
        /// </summary>
        /// <param name="candidato"></param>
        /// <returns></returns>
        /// <response code="201">Un candidato recién creado</response>
        /// <response code="400">Si el candidato es nulo</response>

        [HttpPost]
        public async Task<ActionResult<Candidato>> PostCandidato(Candidato candidato)
        {
            if (_context.Candidatos == null)
            {
                return Problem("Entity set 'RRHHContext.Candidatos'  is null.");
            }
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();
            Log.Information("Método POST: crea un nuevo candidato con sus empleos");
            return CreatedAtAction("GetCandidato", new { id = candidato.Id }, candidato);
        }



        // DELETE: api/Candidato/5

        /// <summary>
        /// Elimina un candidato específico.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">No devuelve contenido en caso de éxito</response>
        /// <response code="404">Si el id del candidato a eliminar no se encuentra como candidato</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidato(int id)
        {
            if (_context.Candidatos == null)
            {
                return NotFound();
            }
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();
            Log.Information("Método DELETE: elimina un candidato con sus empleos");

            return NoContent();
        }

        private bool CandidatoExists(int id)
        {
            return (_context.Candidatos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
