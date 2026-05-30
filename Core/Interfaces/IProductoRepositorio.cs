using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ListaProducto();

        Task<Producto?> BuscarProducto(string codigo);

        Task PostProducto(Producto producto);

        Task PutProducto(Producto producto);

        Task DeleteProducto(string codigo);
    }
}