using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
     [Route("api/[controller]")]
    [ApiController]

    public class CargoSalarioController : ControllerBase
    {
        private readonly ICargoSalarioRepositorio _CargoSalarioRepositorio;
        public CargoSalarioController(ICargoSalarioRepositorio cargoSalarioRepositorio)
        {
            _CargoSalarioRepositorio = cargoSalarioRepositorio;
        }

        [HttpGet("GET")]
        public async Task<IActionResult> GetCargoSalario()
        {
            var cargo = await _CargoSalarioRepositorio.GetCargoSalario();
            return Ok(cargo);
        }
        [HttpPost("POST")]
        public async Task<IActionResult> PostCargoSalario(string codigoSalario , string codigoCargo, string fechaInicio, string fechaFin)
        {
            var cargo = await _CargoSalarioRepositorio.POSTCargoSalario(codigoSalario,codigoCargo,fechaInicio,fechaFin);
            if(cargo == null)
            {
                return BadRequest("La relacion ya existe o los datos son invalidos");
            }
            return Ok( new { mensaje = "El Cargo fue Creado Correctamente" });
        }
        [HttpPatch]
        public async Task<IActionResult> PutCargoSalario(string codigoSalario , string codigoCargo, string fechaInicio, string fechaFin)
        {
            var cargo = await _CargoSalarioRepositorio.PutCargoSalario(codigoSalario,codigoCargo,fechaInicio,fechaFin);
             if(cargo == null)
            {
                return BadRequest("La relacion no existe");
            }
            return Ok( new { mensaje = "El cargo fue actualizado correctamente" });
        }
    }
}