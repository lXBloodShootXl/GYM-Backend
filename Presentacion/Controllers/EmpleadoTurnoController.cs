using Microsoft.AspNetCore.Mvc;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;

namespace GYM.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoTurnoController : ControllerBase
    {
        private readonly IEmpleadoTurnoRepositorio _empleadoTurnoRepositorio;

        public EmpleadoTurnoController(
            IEmpleadoTurnoRepositorio empleadoTurnoRepositorio)
        {
            _empleadoTurnoRepositorio = empleadoTurnoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoTurnoDTO>>> GetEmpleadoTurno()
        {
            var empleadoTurnos =
                await _empleadoTurnoRepositorio.GetEmpleadoTurno();

            return Ok(empleadoTurnos);
        }

        [HttpGet("empleado/{ci}")]
        public async Task<ActionResult<List<EmpleadoTurnoDTO>>> GetTurnosPorEmpleado(
            string ci)
        {
            var turnos =
                await _empleadoTurnoRepositorio.GetTurnosPorEmpleado(ci);

            return Ok(turnos);
        }

        [HttpGet("turno/{codigo}")]
        public async Task<ActionResult<List<EmpleadoTurnoDTO>>> GetEmpleadosPorTurno(
            string codigo)
        {
            var empleados =
                await _empleadoTurnoRepositorio.GetEmpleadosPorTurno(codigo);

            return Ok(empleados);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoTurnoDTO>> PostEmpleadoTurno(
            string ci,
            string codigo)
        {
            var empleadoTurno =
                await _empleadoTurnoRepositorio.PostEmpleadoTurno(ci, codigo);

            if (empleadoTurno == null)
                return BadRequest("Empleado o turno no encontrado.");

            return Ok(empleadoTurno);
        }

        [HttpPut]
        public async Task<ActionResult<EmpleadoTurnoDTO>> PutEmpleadoTurno(
            string ci,
            string codigoAnterior,
            string codigoNuevo)
        {
            var empleadoTurno =
                await _empleadoTurnoRepositorio.PutEmpleadoTurno(
                    ci,
                    codigoAnterior,
                    codigoNuevo);

            if (empleadoTurno == null)
                return NotFound(
                    "No se encontró la asignación o los datos son inválidos.");

            return Ok(empleadoTurno);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmpleadoTurno(
            string ci,
            string codigo)
        {
            var eliminado =
                await _empleadoTurnoRepositorio.DeleteEmpleadoTurno(
                    ci,
                    codigo);

            if (!eliminado)
                return NotFound(
                    "No se encontró la relación empleado-turno.");

            return NoContent();
        }
    }
}