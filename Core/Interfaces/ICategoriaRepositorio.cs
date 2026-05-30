using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> ListaCategoria();

        Task<Categoria?> BuscarCategoria(string codigo);

        Task PostCategoria(Categoria categoria);

        Task PutCategoria(Categoria categoria);

        Task DeleteCategoria(string codigo);
    }
}