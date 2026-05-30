using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapeadores;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioRepositorio _repositorio;

        public InventarioController(IInventarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ListaInventario()
        {
            var inventarios = await _repositorio.ListaInventario();

            return Ok(inventarios.Select(InventarioMapeador.ToDTO));
        }

        [HttpPost]
        public async Task<IActionResult> PostInventario(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest();
            }

            var dto = new InventarioDTO
            {
                codigo = codigo,
                nombre = nombre
            };

            var inventario = InventarioMapeador.ToModel(dto);

            await _repositorio.PostInventario(inventario);

            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteInventario(string codigo)
        {
            await _repositorio.DeleteInventario(codigo);

            return Ok();
        }
    }
}