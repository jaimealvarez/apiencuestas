using apiencuestas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace apiencuestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {
        private readonly EncuestasContext _context;
        public CuestionarioController(EncuestasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPreguntaAleatoria()
        {
            Random rand = new Random();
            int pos = rand.Next(0, _context.Pregunta.Count());

            Preguntum p = await _context.Pregunta.ElementAtAsync(pos);

            var query = (from pr in _context.Pregunta
                        where
                            pr.Id == p.Id
                        select new
                        {
                            IdPregunta = pr.Id,
                            Pregunta = pr.Pregunta,

                            Opciones = (
                            from o in _context.Opcions
                            where
                                o.IdPregunta == pr.Id
                            select new
                            {
                                IdOpcion = o.Id,
                                Opcion = o.Opcion1,
                                Orden = o.Orden
                            }
                            ).ToList()

                        }).FirstOrDefault();

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarRespuesta(Respuesta respuesta)
        {
            await _context.Respuestas.AddAsync(respuesta);
            await _context.SaveChangesAsync();

            var query = (
                from p in _context.Pregunta
                 join c in _context.Opcions on new { v1 = p.Id, v2 = true } equals new { v1 = c.IdPregunta, v2 = (bool)c.Correcta }
                 where c.IdPregunta == respuesta.IdPregunta
                 select new
                 {
                     Id = c.Id,
                     Opcion = c.Opcion1,
                 }

                ).FirstOrDefault();

            return Ok(query);
        }
    }

}
