using Microsoft.AspNetCore.Mvc;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcionsController : ControllerBase
    {
        private readonly ISuscripcionRepositorio _suscripcionRepositorio;

        public SuscripcionsController(ISuscripcionRepositorio suscripcionRepositorio)
        {
            _suscripcionRepositorio = suscripcionRepositorio;
        }

        /// <summary>
        /// Obtiene todas las suscripciones activas.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetSuscripcion()
        {
            var suscripciones = await _suscripcionRepositorio.GetSuscripcion();

            return Ok(suscripciones);
        }

        /// <summary>
        /// Obtiene las suscripciones activas de una persona.
        /// </summary>
        [HttpGet("GET/{ci}")]
        public async Task<IActionResult> GetSuscripcion(string ci)
        {
            if (string.IsNullOrWhiteSpace(ci))
                return BadRequest("CI inválido.");

            var suscripciones = await _suscripcionRepositorio.GetSuscripcion(ci);

            if (suscripciones == null || !suscripciones.Any())
                return NotFound($"No se encontraron suscripciones para el CI {ci}.");

            return Ok(suscripciones);
        }

        /// <summary>
        /// Obtiene una suscripción específica de una persona.
        /// </summary>
        [HttpGet("GET/{ci}/{codigo}")]
        public async Task<IActionResult> GetSuscripcionMembresia(
            string ci,
            string codigo)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo)
            )
                return BadRequest("Faltan campos.");

            var suscripciones = await _suscripcionRepositorio
                .GetSuscripcionMembresia(ci, codigo);

            if (suscripciones == null || !suscripciones.Any())
                return NotFound(
                    $"No se encontró la suscripción {codigo} para {ci}.");

            return Ok(suscripciones);
        }

        /// <summary>
        /// Obtiene las suscripciones borradas.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetSuscripcionBorrados()
        {
            var suscripciones = await _suscripcionRepositorio
                .GetSuscripcionBorrados();

            return Ok(suscripciones);
        }

        /// <summary>
        /// Crea una nueva suscripción.
        /// </summary>
        [HttpPost("POST")]
        public async Task<IActionResult> PostSuscripcion(
            string ci,
            string codigo,
            string fecha_inicio,
            string fecha_fin)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(fecha_inicio) ||
                string.IsNullOrWhiteSpace(fecha_fin)
            )
                return BadRequest("Faltan campos.");

            var suscripcionCreada =
                await _suscripcionRepositorio.PostSuscripcion(
                    ci,
                    codigo,
                    fecha_inicio,
                    fecha_fin
                );

            if (suscripcionCreada == null)
            {
                return BadRequest(
                    "No se pudo crear la suscripción.");
            }

            return CreatedAtAction(
                nameof(GetSuscripcionMembresia),
                new
                {
                    ci = suscripcionCreada.ci,
                    codigo = suscripcionCreada.codigo
                },
                suscripcionCreada
            );
        }

        /// <summary>
        /// Actualiza una suscripción existente.
        /// </summary>
        [HttpPut("PUT")]
        public async Task<IActionResult> PutSuscripcion(
            string ci,
            string codigo,
            string fecha_inicio,
            string fecha_fin)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(fecha_inicio) ||
                string.IsNullOrWhiteSpace(fecha_fin)
            )
                return BadRequest("Faltan campos.");

            var suscripcion =
                await _suscripcionRepositorio.PutSuscripcion(
                    ci,
                    codigo,
                    fecha_inicio,
                    fecha_fin
                );

            if (suscripcion == null)
            {
                return NotFound(
                    "No se encontró la suscripción.");
            }

            return Ok(suscripcion);
        }

        /// <summary>
        /// Elimina lógicamente una suscripción.
        /// </summary>
        [HttpDelete("DEL/{ci}/{codigo}")]
        public async Task<IActionResult> DeleteSuscripcion(
            string ci,
            string codigo)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo)
            )
                return BadRequest("Faltan campos.");

            var suscripcion =
                await _suscripcionRepositorio
                    .DeleteSuscripcion(ci, codigo);

            if (suscripcion == null)
            {
                return NotFound(
                    "No se encontró la suscripción.");
            }

            return Ok(suscripcion);
        }

        /// <summary>
        /// Reactiva una suscripción eliminada lógicamente.
        /// </summary>
        [HttpPut("HAB/{ci}/{codigo}")]
        public async Task<IActionResult> HabilitarSuscripcion(
            string ci,
            string codigo)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo)
            )
                return BadRequest("Faltan campos.");

            var suscripcion =
                await _suscripcionRepositorio
                    .HabilitarSuscripcion(ci, codigo);

            if (suscripcion == null)
            {
                return NotFound(
                    "No se encontró la suscripción.");
            }

            return Ok(suscripcion);
        }
    }
}