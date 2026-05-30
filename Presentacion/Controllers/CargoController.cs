using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
     [Route("api/[controller]")]
    [ApiController]

    public class CargoController : ControllerBase
    {
        private readonly ICargoRepositorio _CargoRepositorio;
        public CargoController(ICargoRepositorio cargoRepositorio)
        {
            _CargoRepositorio = cargoRepositorio;
        }

        [HttpGet("GET")]
        public async Task<IActionResult> GetCargo()
        {
            var cargo = await _CargoRepositorio.GetCargos();
            return Ok(cargo);
        }
        [HttpPost("POST")]
        public async Task<IActionResult> PostCargo(string codigo , string nombre)
        {
            var cargo = await _CargoRepositorio.POSTCargos(codigo,nombre);
            return Ok( new { mensaje = "El Cargo fue Creado Correctamente" });
        }
        [HttpPut("PUT")]
        public async Task<IActionResult> PutCargo (string codigo, string nombre)
        {
            var cargo = await _CargoRepositorio.PatchCargos(codigo, nombre);
            return Ok(new { mensaje = "El Cargo se actualiazo correctamente" });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCargos(string codigo)
        {
            var cargo = await _CargoRepositorio.DeleteCargos(codigo);
            return Ok( new { mensaje = "Salario Desactivado Correctamente" });
        }
    }
}