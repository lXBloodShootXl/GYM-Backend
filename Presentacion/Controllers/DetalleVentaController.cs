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
    public class DetalleVentaController : ControllerBase
    {
        private readonly IDetalleVentaRepositorio _repositorio;
        private readonly GYM_DBContext _context;

        public DetalleVentaController(
            IDetalleVentaRepositorio repositorio,
            GYM_DBContext context)
        {
            _repositorio = repositorio;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListaDetalleVenta()
        {
            var detalles = await _repositorio.ListaDetalleVenta();

            return Ok(detalles);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string codigoVenta, string codigoProducto, int cantidad)
        {
            var venta = await _context.Ventas
                .FirstOrDefaultAsync(v => v.codigo == codigoVenta);

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.codigo == codigoProducto);

            if (venta == null || producto == null)
            {
                return BadRequest("Venta o Producto no existe");
            }

            DetalleVenta detalle = new DetalleVenta
            {
                id_venta = venta.id_venta,
                id_producto = producto.id_producto,
                cantidad = cantidad
            };

            await _repositorio.Post(detalle);

            return Ok();
        }

        [HttpDelete("{codigoVenta}/{codigoProducto}")]
        public async Task<IActionResult> Delete(
            string codigoVenta,
            string codigoProducto)
        {
            await _repositorio.Delete(codigoVenta, codigoProducto);

            return Ok();
        }
    }
}