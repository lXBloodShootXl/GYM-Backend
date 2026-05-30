using Microsoft.AspNetCore.Mvc;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepositorio _PersonaRepositorio;

        public PersonasController(IPersonaRepositorio PersonaRepositorio)
        {
            _PersonaRepositorio = PersonaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Personas activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetPersona()
        {
            var Personas = await _PersonaRepositorio.GetPersona();
            return Ok(Personas);
        }
        /// <summary>
        /// Obtiene un Persona por su CI.
        /// </summary>
        [HttpGet("GET/{ci}")]
        public async Task<IActionResult> GetPersona(string ci)
        {
            var Persona = await _PersonaRepositorio.GetPersona(ci);
            if (Persona is null)
                return NotFound($"No se encontró un Persona con CI {ci}.");

            return Ok(Persona);
        }

        /// <summary>
        /// Obtiene la lista de Personas marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetPersonasBorrados()
        {
            var Personas = await _PersonaRepositorio.GetPersonaBorrados();
            return Ok(Personas);
        }

        /// <summary>
        /// Crea un nuevo Persona.
        /// </summary>
        [HttpPost("POST")]
        public async Task<IActionResult> PostPersona(string ci, string nombre, string? apellido_p, string? apellido_m, bool sexo, string fecha_nacimiento, string hashhuella)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre) || (string.IsNullOrWhiteSpace(apellido_p) && string.IsNullOrWhiteSpace(apellido_m)) || string.IsNullOrWhiteSpace(fecha_nacimiento))
                return BadRequest("Faltan campos.");

            var PersonaCreado = await _PersonaRepositorio.PostPersona(ci, nombre, apellido_p, apellido_m, sexo, fecha_nacimiento, hashhuella);

            return CreatedAtAction(nameof(GetPersona), new { ci = PersonaCreado.ci }, PersonaCreado);
        }

        /// <summary>
        /// Actualiza un Persona existente.
        /// </summary>
        [HttpPatch("PATCH")]
        public async Task<IActionResult> PutPersona(string ci, string? nombre, string? apellido_p, string? apellido_m, bool? sexo, string? fecha_nacimiento, string? hashhuella)
        {
            if (string.IsNullOrWhiteSpace(ci))
                return BadRequest("Faltan CI.");
            if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(apellido_p) && string.IsNullOrWhiteSpace(apellido_m) && string.IsNullOrWhiteSpace(fecha_nacimiento))
                return Ok("No hay nada que cambiar.");

            var Persona = await _PersonaRepositorio.PutPersona(ci, nombre, apellido_p, apellido_m, sexo, fecha_nacimiento, hashhuella);
            if (Persona is null)
                return NotFound($"No se encontró el Persona con CI {ci}.");

            return Ok(Persona);
        }

        /// <summary>
        /// Marca un Persona como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{ci}")]
        public async Task<IActionResult> DeletePersona(string ci)
        {
            var PersonaEliminado = await _PersonaRepositorio.DeletePersona(ci);
            if (PersonaEliminado is null)
                return NotFound($"No se encontró un Persona con CI {ci}.");

            return Ok(PersonaEliminado);
        }
        /// <summary>
        /// Habilita un Persona previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPatch("HAB/{ci}")]
        public async Task<IActionResult> HabilitarPersona(string ci)
        {
            var Persona = await _PersonaRepositorio.HabilitarPersona(ci);

            if (Persona is null)
                return NotFound($"No se encontró un Persona con CI {ci}.");

            return Ok(Persona);
        }
    }
}
