using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using GYM.Core.Interfaces;

namespace GYM.Infraestructura.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly GYM_DBContext _context;

        public ProductoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ListaProducto()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Stocks)
                    .ThenInclude(s => s.inventario)
                .ToListAsync();
        }

        public async Task<Producto?> BuscarProducto(string codigo)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Stocks)
                    .ThenInclude(s => s.inventario)
                .FirstOrDefaultAsync(p => p.codigo == codigo);
        }

        public async Task PostProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task PutProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProducto(string codigo)
        {
            var producto = await BuscarProducto(codigo);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}