using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM.Infraestructura.Repositorio
{
    public class StockRepositorio : IStockRepositorio
    {
        private readonly GYM_DBContext _context;

        public StockRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> ListaStock()
        {
            return await _context.Stocks
                .Include(s => s.inventario)
                .Include(s => s.producto)
                .ToListAsync();
        }

        public async Task Post(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string codigoInventario, string codigoProducto)
        {
            var stock = await _context.Stocks
                .Include(s => s.inventario)
                .Include(s => s.producto)
                .FirstOrDefaultAsync(s =>
                    s.inventario.codigo == codigoInventario &&
                    s.producto.codigo == codigoProducto);

            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}