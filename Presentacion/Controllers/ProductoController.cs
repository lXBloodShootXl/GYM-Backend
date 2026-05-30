using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _repositorio;
        private readonly GYM_DBContext _context;

        public ProductoController(
            IProductoRepositorio repositorio,
            GYM_DBContext context)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListaProducto()
        {
            var productos = await _repositorio.ListaProducto();

            var datos = productos.Select(p => new ProductoDTO
            {
                codigo = p.codigo,
                nombre = p.nombre,
                descripcion = p.descripcion,
                precio = p.precio,
                codigoCategoria = p.Categoria?.codigo ?? "" 
            });

            return Ok(datos);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto(string codigo, string nombre, string descripcion, decimal precio, string codigoCategoria)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.codigo == codigoCategoria);

            if (categoria == null)
            {
                return BadRequest("Categoria no existe");
            }

            Producto producto = new Producto
            {
                codigo = codigo,
                nombre = nombre,
                descripcion = descripcion,
                precio = precio,
                id_categoria = categoria.id_categoria
            };

            await _repositorio.PostProducto(producto);

            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteProducto(string codigo)
        {
            await _repositorio.DeleteProducto(codigo);

            return Ok();
        }
    }
}