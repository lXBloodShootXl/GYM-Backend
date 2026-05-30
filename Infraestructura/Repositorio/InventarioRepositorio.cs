using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace RRHH.Infraestructura.Repositorio
{
    public class InventarioRepositorio : IInventarioRepositorio
    {
        private readonly GYM_DBContext _context;

        public InventarioRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Inventario>> ListaInventario()
        {
            return await _context.Inventarios.ToListAsync();
        }

        public async Task<Inventario?> BuscarInventario(string codigo)
        {
            return await _context.Inventarios
                .FirstOrDefaultAsync(i => i.codigo == codigo);
        }

        public async Task PostInventario(Inventario inventario)
        {
            await _context.Inventarios.AddAsync(inventario);
            await _context.SaveChangesAsync();
        }

        public async Task PutInventario(Inventario inventario)
        {
            _context.Inventarios.Update(inventario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInventario(string codigo)
        {
            var inventario = await BuscarInventario(codigo);

            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
                await _context.SaveChangesAsync();
            }
        }
    }
}