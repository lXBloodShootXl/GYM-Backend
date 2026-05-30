using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
     [Route("api/[controller]")]
    [ApiController]

    public class SalarioController : ControllerBase
    {
        private readonly ISalarioRepositorio _SalarioRepositorio;
        public SalarioController(ISalarioRepositorio salarioRepositorio)
        {
            _SalarioRepositorio = salarioRepositorio;
        }

        [HttpGet("GET")]
        public async Task<IActionResult> GetSalario()
        {
            var salarios = await _SalarioRepositorio.GetSalarios();
            return Ok(salarios);
        }
        [HttpPost("POST")]
        public async Task<IActionResult> PostSalarios(string codigo , int salarioo)
        {
            var salario = await _SalarioRepositorio.PostSalario(codigo,salarioo);
            return Ok( new { mensaje = "Usuario Creado Correctamente" });
        }
        [HttpPut("PUT")]
        public async Task<IActionResult> PutSalarios (string codigo, int salarioo)
        {
            var salario = await _SalarioRepositorio.PatchSalario(codigo, salarioo);
            return Ok(new { mensaje = "El usuarios se actualiazo correctamente" });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSalarios(string codigo)
        {
            var salario = await _SalarioRepositorio.DeleteSalario(codigo);
            return Ok( new { mensaje = "Salario Desactivado Correctamente" });
        }
    }
}