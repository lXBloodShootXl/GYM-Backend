using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepositorio _repositorio;
        private readonly GYM_DBContext _context;

        public StockController(IStockRepositorio repositorio, GYM_DBContext context)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListaStock()
        {
            var stocks = await _repositorio.ListaStock();

            var datos = stocks.Select(s => new StockDTO
            {
                codigoInventario = s.inventario?.codigo ?? "", 
                codigoProducto = s.producto?.codigo ?? "", 
                cantidad = s.cantidad
            });

            return Ok(datos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string codigoInventario, string codigoProducto, int cantidad)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.codigo == codigoInventario);

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.codigo == codigoProducto);

            if (inventario == null || producto == null)
            {
                return BadRequest("Inventario o Producto no existe");
            }

            Stock stock = new Stock
            {
                id_inventario = inventario.id_inventario,
                id_producto = producto.id_producto,
                cantidad = cantidad
            };

            await _repositorio.Post(stock);

            return Ok();
        }

        [HttpDelete("{codigoInventario}/{codigoProducto}")]
        public async Task<IActionResult> Delete(string codigoInventario, string codigoProducto)
        {
            await _repositorio.Delete(codigoInventario, codigoProducto);

            return Ok();
        }
    }
}