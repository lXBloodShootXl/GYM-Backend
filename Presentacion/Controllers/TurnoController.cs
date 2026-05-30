using Microsoft.AspNetCore.Mvc;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepositorio _turnoRepositorio;

        public TurnoController(ITurnoRepositorio turnoRepositorio)
        {
            _turnoRepositorio = turnoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TurnoDTO>>> GetTurno()
        {
            var turnos = await _turnoRepositorio.GetTurno();

            return Ok(turnos);
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TurnoDTO>> GetTurnoByCodigo(string codigo)
        {
            var turno = await _turnoRepositorio.GetTurnoByCodigo(codigo);

            if (turno == null)
                return NotFound();

            return Ok(turno);
        }

        [HttpPost]
        public async Task<ActionResult<TurnoDTO>> PostTurno(string codigo, string nombre, string hora_inicio, string hora_fin)
        {
            var turno = await _turnoRepositorio.PostTurno(
                codigo,
                nombre,
                hora_inicio,
                hora_fin
            );

            if (turno == null)
                return BadRequest();

            return Ok(turno);
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult<TurnoDTO>> PutTurno(
            string codigo,
            string nombre,
            string hora_inicio,
            string hora_fin)
        {
            var turno = await _turnoRepositorio.PutTurno(
                codigo,
                nombre,
                hora_inicio,
                hora_fin
            );

            if (turno == null)
                return NotFound();

            return Ok(turno);
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> DeleteTurno(string codigo)
        {
            var eliminado = await _turnoRepositorio.DeleteTurno(codigo);

            if (!eliminado)
                return NotFound();

            return Ok("Turno eliminado correctamente");
        }
    }
}