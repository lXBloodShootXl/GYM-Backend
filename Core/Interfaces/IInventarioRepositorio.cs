using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface IInventarioRepositorio
    {
        Task<List<Inventario>> ListaInventario();

        Task<Inventario?> BuscarInventario(string  codigo);

        Task PostInventario(Inventario inventario);

        Task PutInventario(Inventario inventario);

        Task DeleteInventario(string codigo);
    }
}