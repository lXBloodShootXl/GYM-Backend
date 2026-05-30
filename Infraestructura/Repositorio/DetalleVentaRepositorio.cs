using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM.Infraestructura.Repositorio
{
    public class DetalleVentaRepositorio : IDetalleVentaRepositorio
    {
        private readonly GYM_DBContext _context;

        public DetalleVentaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleVenta>> ListaDetalleVenta()
        {
            return await _context.DetalleVentas
                .Include(d => d.ventas)
                .Include(d => d.producto)
                .ToListAsync();
        }

        public async Task Post(DetalleVenta detalleVenta)
        {
            await _context.DetalleVentas.AddAsync(detalleVenta);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string codigoVenta, string codigoProducto)
        {
            var detalle = await _context.DetalleVentas
                .Include(d => d.ventas)
                .Include(d => d.producto)
                .FirstOrDefaultAsync(d =>
                    d.ventas.codigo == codigoVenta &&
                    d.producto.codigo == codigoProducto);

            if (detalle != null)
            {
                _context.DetalleVentas.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}