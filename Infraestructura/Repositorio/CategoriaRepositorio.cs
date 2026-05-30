using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace RRHH.Infraestructura.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly GYM_DBContext _context;

        public CategoriaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ListaCategoria()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> BuscarCategoria(string codigo)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.codigo == codigo);
        }

        public async Task PostCategoria(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task PutCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoria(string codigo)
        {
            var categoria = await BuscarCategoria(codigo);

            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}