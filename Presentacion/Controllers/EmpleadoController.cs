using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Infraestructura.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepositorio _empleadoRepositorio;

        public EmpleadoController(IEmpleadoRepositorio empleadoRepositorio)
        {
            _empleadoRepositorio = empleadoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoDTO>>> GetEmpleado()
        {
            var empleados = await _empleadoRepositorio.GetEmpleado();

            return Ok(empleados);
        }

        [HttpPost("{ci}")]
        public async Task<ActionResult<EmpleadoDTO>> PostEmpleado(string ci, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd))
                return BadRequest("CI y contraseña son requeridos");
            var empleado = await _empleadoRepositorio.PostEmpleado(ci, pwd);

            if (empleado == null)
                return NotFound("Persona no encontrada");

            return Ok(empleado);
        }

        [HttpPut("PUT")]
        public async Task<IActionResult> PutTelefono(string ci, string pwd, string nuevo_pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd) || string.IsNullOrWhiteSpace(nuevo_pwd))
                return BadRequest("Faltan campos.");

            var emp = await _empleadoRepositorio.PutEmpleado(ci, pwd, nuevo_pwd);
            if (emp is null)
                return NotFound($"No se encontró el empleado con ci {ci}.");

            return Ok(emp);
        }

        [HttpDelete("{ci}")]
        public async Task<ActionResult> DeleteEmpleado(string ci)
        {
            var eliminado = await _empleadoRepositorio.DeleteEmpleado(ci);

            if (!eliminado)
                return NotFound("Empleado no encontrado");

            return Ok("Empleado eliminado correctamente");
        }

        [HttpPut("{ci}")]
        public async Task<ActionResult<EmpleadoDTO>> PutEmpleado( string ci, DateOnly fecha)
        {
            var empleado = await _empleadoRepositorio.PutEmpleado(ci, fecha);

            if (empleado == null)
                return NotFound("Empleado no encontrado");

            return Ok(empleado);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginEmpleado(string ci, string pwd)
        {
            var Clientes = await _empleadoRepositorio.LoginEmpleado(ci, pwd);
            if (Clientes == false)
                return NotFound("Sin acceso o no existente");
            else
                return Ok("Inicio de sesión exitoso.");
        }
    }
}