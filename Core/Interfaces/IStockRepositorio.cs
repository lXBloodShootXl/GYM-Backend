using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface IStockRepositorio
    {
        Task<List<Stock>> ListaStock();

        Task Post(Stock stock);

        Task Delete(string codigoInventario, string codigoProducto);
    }
}