using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapeadores;
using Microsoft.AspNetCore.Mvc;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepositorio _repositorio;

        public CategoriaController(ICategoriaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ListaCategoria()
        {
            var categorias = await _repositorio.ListaCategoria();

            return Ok(categorias.Select(CategoriaMapeador.ToDTO));
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoria(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El código y el nombre son obligatorios.");
            }

            
            var dto = new CategoriaDTO
            {
                codigo = codigo,
                nombre = nombre,
                descripcion = descripcion
            };

            var categoria = CategoriaMapeador.ToModel(dto);

            await _repositorio.PostCategoria(categoria);

            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteCategoria(string codigo)
        {
            await _repositorio.DeleteCategoria(codigo);

            return Ok();
        }
    }
}