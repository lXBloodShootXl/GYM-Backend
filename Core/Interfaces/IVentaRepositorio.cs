using DTOS;
using Models;

namespace Interfaces
{
    public interface IVentaRepositorio
    {
        Task<List<Ventas>> ListaVentas();

        Task<Ventas?> BuscarVenta(int id);

        Task Post(Ventas ventas);

        Task Put(Ventas ventas);

        Task Delete(string codigo);
    }
}