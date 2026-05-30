using GYM.Core.Interfaces;
using GYM.Infraestructura.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _ClienteRepositorio;

        public ClientesController(IClienteRepositorio ClienteRepositorio)
        {
            _ClienteRepositorio = ClienteRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de Clientes activos.
        /// </summary>
        [HttpGet("GET")]
        public async Task<IActionResult> GetCliente()
        {
            var Clientes = await _ClienteRepositorio.GetCliente();
            return Ok(Clientes);
        }
        /// <summary>
        /// Obtiene un Cliente por su ci.
        /// </summary>
        [HttpGet("GET/{ci}")]
        public async Task<IActionResult> GetCliente(string ci)
        {
            var Cliente = await _ClienteRepositorio.GetCliente(ci);
            if (Cliente is null)
                return NotFound($"No se encontró un Cliente con ci {ci}.");

            return Ok(Cliente);
        }

        /// <summary>
        /// Obtiene la lista de Clientes marcados como borrados.
        /// </summary>
        [HttpGet("GET/Borrados")]
        public async Task<IActionResult> GetClientesBorrados()
        {
            var Clientes = await _ClienteRepositorio.GetClienteBorrados();
            return Ok(Clientes);
        }

        /// <summary>
        /// Crea un nuevo Cliente.
        /// </summary>
        [HttpPost("POST/{ci}")]
        public async Task<IActionResult> PostCliente(string ci, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd))
                return BadRequest("Faltan campos.");

            var ClienteCreado = await _ClienteRepositorio.PostCliente(ci, pwd);

            return CreatedAtAction(nameof(GetCliente), new { ci = ClienteCreado.ci }, ClienteCreado);
        }

        [HttpPut("PUT")]
        public async Task<IActionResult> PutTelefono(string ci, string pwd, string nuevo_pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd) || string.IsNullOrWhiteSpace(nuevo_pwd))
                return BadRequest("Faltan campos.");

            var emp = await _ClienteRepositorio.PutCliente(ci, pwd, nuevo_pwd);
            if (emp is null)
                return NotFound($"No se encontró el cliente con ci {ci}.");

            return Ok(emp);
        }

        /// <summary>
        /// Marca un Cliente como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("DEL/{ci}")]
        public async Task<IActionResult> DeleteCliente(string ci)
        {
            var ClienteEliminado = await _ClienteRepositorio.DeleteCliente(ci);
            if (ClienteEliminado is null)
                return NotFound($"No se encontró un Cliente {ci}.");

            return Ok(ClienteEliminado);
        }
        /// <summary>
        /// Habilita un Cliente previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPatch("HAB/{ci}")]
        public async Task<IActionResult> HabilitarCliente(string ci)
        {
            var Cliente = await _ClienteRepositorio.HabilitarCliente(ci);

            if (Cliente is null)
                return NotFound($"No se encontró un Cliente {ci}.");

            return Ok(Cliente);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginCliente(string ci, string pwd)
        {
            var Clientes = await _ClienteRepositorio.LoginCliente(ci, pwd);
            if (Clientes == false)
                return NotFound("Sin acceso o no existente");
            else
                return Ok("Inicio de sesión exitoso.");
        }
    }
}   