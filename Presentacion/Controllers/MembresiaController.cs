using Microsoft.AspNetCore.Mvc;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiaRepositorio _MembresiaRepositorio;

        public MembresiasController(IMembresiaRepositorio MembresiaRepositorio)
        {
            _MembresiaRepositorio = MembresiaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Membresias activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetMembresia()
        {
            var Membresias = await _MembresiaRepositorio.GetMembresia();
            return Ok(Membresias);
        }
        /// <summary>
        /// Obtiene un Membresia por su codigo.
        /// </summary>
        [HttpGet("GET/{codigo}")]
        public async Task<IActionResult> GetMembresia(string codigo)
        {
            var Membresia = await _MembresiaRepositorio.GetMembresia(codigo);
            if (Membresia is null)
                return NotFound($"No se encontró un Membresia con codigo {codigo}.");

            return Ok(Membresia);
        }

        /// <summary>
        /// Obtiene la lista de Membresias marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetMembresiasBorrados()
        {
            var Membresias = await _MembresiaRepositorio.GetMembresiaBorrados();
            return Ok(Membresias);
        }

        /// <summary>
        /// Crea un nuevo Membresia.
        /// </summary>
        [HttpPost("POST")]
        public async Task<IActionResult> PostMembresia(string codigo, string nombre, int duracion, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || duracion < 30 || precio <= 0)
                return BadRequest("Faltan campos o la Duración debe ser mayor o igual a 30 días y Precio mayor a 0.");

            var MembresiaCreado = await _MembresiaRepositorio.PostMembresia(codigo, nombre, duracion, precio);

            return CreatedAtAction(nameof(GetMembresia), new { codigo = MembresiaCreado.codigo }, MembresiaCreado);
        }

        /// <summary>
        /// Actualiza un Membresia existente.
        /// </summary>
        [HttpPatch("PATCH")]
        public async Task<IActionResult> PutMembresia(string codigo, string? nombre, int? duracion, decimal? precio)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return BadRequest("Faltan campos o la Duración debe ser mayor o igual a 30 días y Precio mayor a 0.");
            if (string.IsNullOrWhiteSpace(nombre) && duracion == null && precio == null)
                return Ok("No hubo nada que actualizar.");
            var Membresia = await _MembresiaRepositorio.PutMembresia(codigo, nombre, duracion, precio);
            if (Membresia is null)
                return NotFound($"No se encontró el Membresia {codigo}.");

            return Ok(Membresia);
        }

        /// <summary>
        /// Marca un Membresia como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{codigo}")]
        public async Task<IActionResult> DeleteMembresia(string codigo)
        {
            var MembresiaEliminado = await _MembresiaRepositorio.DeleteMembresia(codigo);
            if (MembresiaEliminado is null)
                return NotFound($"No se encontró un Membresia {codigo}.");

            return Ok(MembresiaEliminado);
        }
        /// <summary>
        /// Habilita un Membresia previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("HAB/{codigo}")]
        public async Task<IActionResult> HabilitarMembresia(string codigo)
        {
            var Membresia = await _MembresiaRepositorio.HabilitarMembresia(codigo);

            if (Membresia is null)
                return NotFound($"No se encontró un Membresia {codigo}.");

            return Ok(Membresia);
        }
    }
}
