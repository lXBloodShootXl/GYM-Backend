using Microsoft.AspNetCore.Mvc;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly IAsistenciaRepositorio _AsistenciaRepositorio;

        public AsistenciasController(IAsistenciaRepositorio AsistenciaRepositorio)
        {
            _AsistenciaRepositorio = AsistenciaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Asistencias activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetAsistencia()
        {
            var Asistencias = await _AsistenciaRepositorio.GetAsistencia();
            return Ok(Asistencias);
        }
        /// <summary>
        /// Obtiene un Asistencia por su fecha.
        /// </summary>
        [HttpGet("GET/{fecha}")]
        public async Task<IActionResult> GetAsistencia(string fecha)
        {
            var Asistencia = await _AsistenciaRepositorio.GetAsistencia(fecha);
            if (Asistencia is null)
                return NotFound($"No se encontró un Asistencia con fecha {fecha}.");

            return Ok(Asistencia);
        }

        /// <summary>
        /// Obtiene la lista de Asistencias marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetAsistenciasBorrados()
        {
            var Asistencias = await _AsistenciaRepositorio.GetAsistenciaBorrados();
            return Ok(Asistencias);
        }

        /// <summary>
        /// Crea una nueva asistencia.
        /// Si no se envía fecha, se usa la fecha actual.
        /// </summary>
        [HttpPost("POST")]
        public async Task<IActionResult> PostAsistencia(string? fecha)
        {
            bool usoFechaActual = string.IsNullOrWhiteSpace(fecha);

            var asistenciaCreada = await _AsistenciaRepositorio
                .PostAsistencia(fecha);

            if (asistenciaCreada == null)
                return BadRequest("La fecha proporcionada no es válida.");

            return CreatedAtAction(
                nameof(GetAsistencia),
                new { fecha = asistenciaCreada.fecha },
                new
                {
                    mensaje = usoFechaActual
                        ? "Se usó la fecha actual."
                        : "Asistencia creada correctamente.",
                    data = asistenciaCreada
                }
            );
        }

        /// <summary>
        /// Actualiza un Asistencia existente.
        /// </summary>
        /*[HttpPut("PUT/{fecha}/{fecha_nuevo}")]
        public async Task<IActionResult> PutAsistencia(string fecha, string fecha_nuevo)
        {
            if (string.IsNullOrWhiteSpace(fecha) || string.IsNullOrWhiteSpace(fecha_nuevo))
                return BadRequest("Faltan campos.");

            var Asistencia = await _AsistenciaRepositorio.PutAsistencia(fecha, fecha_nuevo);
            if (Asistencia is null)
                return NotFound($"No se encontró el Asistencia {fecha}.");

            return Ok(Asistencia);
        }*/

        /// <summary>
        /// Marca un Asistencia como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{fecha}")]
        public async Task<IActionResult> DeleteAsistencia(string fecha)
        {
            var AsistenciaEliminado = await _AsistenciaRepositorio.DeleteAsistencia(fecha);
            if (AsistenciaEliminado is null)
                return NotFound($"No se encontró un Asistencia {fecha}.");

            return Ok(AsistenciaEliminado);
        }
        /// <summary>
        /// Habilita un Asistencia previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("HAB/{fecha}")]
        public async Task<IActionResult> HabilitarAsistencia(string fecha)
        {
            var Asistencia = await _AsistenciaRepositorio.HabilitarAsistencia(fecha);

            if (Asistencia is null)
                return NotFound($"No se encontró un Asistencia {fecha}.");

            return Ok(Asistencia);
        }
    }
}
