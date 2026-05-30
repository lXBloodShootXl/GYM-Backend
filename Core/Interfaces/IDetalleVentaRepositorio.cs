using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface IDetalleVentaRepositorio
    {
        Task<List<DetalleVenta>> ListaDetalleVenta();

        Task Post(DetalleVenta detalleVenta);

        Task Delete(string codigoVenta, string codigoProducto);
    }
}