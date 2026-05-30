using Microsoft.AspNetCore.Mvc;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaAsistenciaController : ControllerBase
    {
        private readonly IPersonaAsistenciaRepositorio _repo;

        public PersonaAsistenciaController(IPersonaAsistenciaRepositorio repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 📋 Obtener todos los activos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetPersonaAsistencia();
            return Ok(data);
        }

        /// <summary>
        /// 🔍 Obtener por CI y fecha
        /// </summary>
        [HttpGet("{ci}/{fecha}")]
        public async Task<IActionResult> Get(string ci, string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(fecha))
                return BadRequest("CI y fecha son obligatorios.");

            var data = await _repo.GetPersonaAsistencia(ci, fecha);

            if (data is null)
                return NotFound($"No existe relación para CI: {ci} y Fecha: {fecha}");

            return Ok(data);
        }

        /// <summary>
        /// 🗑️ Obtener borrados
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetBorrados()
        {
            var data = await _repo.GetPersonaAsistenciaBorrados();
            return Ok(data);
        }

        /// <summary>
        /// ➕ Crear relación Persona-Fecha
        /// </summary>
        [HttpPost("{ci}")]
        public async Task<IActionResult> Post(string ci, string? fecha)
        {
            if (string.IsNullOrWhiteSpace(ci))
                return BadRequest("CI obligatorio.");

            var creado = await _repo.PostPersonaAsistencia(ci, fecha);

            if (creado is null)
                return BadRequest("No se pudo crear (persona/fecha inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { ci, fecha }, creado);
        }
        [HttpPost("/Huella")]
        public async Task<IActionResult> PostHuella(string huella, string? fecha)
        {
            if (string.IsNullOrWhiteSpace(huella))
                return BadRequest("Huella obligatoria.");

            var creado = await _repo.PostPersonaAsistenciaHuella(huella, fecha);

            if (creado is null)
                return BadRequest("No se pudo crear (persona/fecha inexistente o duplicado).");

            return CreatedAtAction(nameof(Get), new { huella, fecha }, creado);
        }

        /// <summary>
        /// ✏️ Actualizar (fecha_fin)
        /// </summary>
        /*[HttpPatch("{ci}/{fecha}")]
        public async Task<IActionResult> Put(string ci, string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(fecha))
                return BadRequest("Datos incompletos.");

            var actualizado = await _repo.PutPersonaAsistencia(ci, fecha);

            if (actualizado is null)
                return NotFound("Relación no encontrada.");

            return Ok(actualizado);
        }*/

        /// <summary>
        /// 🗑️ Eliminación lógica
        /// </summary>
        [HttpDelete("{ci}/{fecha}")]
        public async Task<IActionResult> Delete(string ci, string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(fecha))
                return BadRequest("CI y fecha son obligatorios.");

            var eliminado = await _repo.DeletePersonaAsistencia(ci, fecha);

            if (eliminado is null)
                return NotFound("Relación no encontrada o ya eliminada.");

            return Ok(eliminado);
        }

        /// <summary>
        /// ♻️ Habilitar registro borrado
        /// </summary>
        [HttpPatch("habilitar/{ci}/{fecha}")]
        public async Task<IActionResult> Habilitar(string ci, string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(fecha))
                return BadRequest("CI y fecha son obligatorios.");

            var habilitado = await _repo.HabilitarPersonaAsistencia(ci, fecha);

            if (habilitado is null)
                return NotFound("No se encontró el registro borrado.");

            return Ok(habilitado);
        }
    }
}