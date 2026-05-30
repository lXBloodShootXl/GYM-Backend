using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GYM.Infraestructura.Repositorio
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly GYM_DBContext _context;

        public VentaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Ventas>> ListaVentas()
        {
            return await _context.Ventas
                .Include(v => v.Empleado)
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        public async Task<Ventas?> BuscarVenta(int id)
        {
            return await _context.Ventas
                .Include(v => v.Empleado)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.id_venta == id);
        }

        public async Task Post(Ventas ventas)
        {
            await _context.Ventas.AddAsync(ventas);
            await _context.SaveChangesAsync();
        }

        public async Task Put(Ventas ventas)
        {
            _context.Ventas.Update(ventas);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string codigo)
        {
            var venta = await _context.Ventas
                .FirstOrDefaultAsync(v => v.codigo == codigo);

            if (venta == null)
                return;

            _context.Ventas.Remove(venta);

            await _context.SaveChangesAsync();
        }
    }
}