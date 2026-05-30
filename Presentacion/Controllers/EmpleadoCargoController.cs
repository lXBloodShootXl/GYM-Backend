using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
     [Route("api/[controller]")]
    [ApiController]

    public class EmpleadoCargoController : ControllerBase
    {
        private readonly IEmpleadoCargoRepositorio _EmpleadoCargoRepositorio;
        public EmpleadoCargoController(IEmpleadoCargoRepositorio EmpleadocargoRepositorio)
        {
            _EmpleadoCargoRepositorio = EmpleadocargoRepositorio;
        }

        [HttpGet("GET")]
        public async Task<IActionResult> GetEmpleadoCargo()
        {
            var cargo = await _EmpleadoCargoRepositorio.GetEmpleadoCargoSalario();
            return Ok(cargo);
        }
        [HttpPost("POST")]
        public async Task<IActionResult> PostEmpleadoCargo(string codigoEmpleado , string codigoCargo, string fechaInicio, string fechaFin)
        {
            var cargo = await _EmpleadoCargoRepositorio.POSTEmpleadoCargoSalario(codigoEmpleado,codigoCargo,fechaInicio,fechaFin);
            if(cargo == null)
            {
                return BadRequest("No se pudo crear la relacion");
            }
            return Ok( new { mensaje = "El Cargo fue Creado Correctamente" });
        }
        [HttpPatch("Actualizar")]
        public async Task<IActionResult> PutEmpleadosCargo(string codigoEmpleado , string codigoCargo, string fechaInicio, string fechaFin)
        {
            var cargo = await _EmpleadoCargoRepositorio.PUTEmpleadoCargoSalario(codigoEmpleado,codigoCargo,fechaInicio,fechaFin);
            if (cargo == null)
            {
                return BadRequest("La relacion no existe");
            }
            return Ok( new { mensaje = "El Cargo fue actualizado  Correctamente" });
        }
        [HttpDelete("Deshabilitar")]
        public async Task<IActionResult> DeshabilitarEmpleado(string codigoEmpleado , string codigoCargo)
        {
            var cargo = await _EmpleadoCargoRepositorio.DeshabilitarCargo(codigoEmpleado,codigoCargo);
            if (cargo == null)
            {
                return BadRequest("No se encontro la relacion");
            }
            return Ok( new { mensaje = "El Cargo fue deshabilitado  Correctamente" });
        }
    }
}