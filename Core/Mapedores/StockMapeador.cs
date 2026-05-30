using GYM.Core.DTOs;
using GYM.Core.Models;

namespace GYM.Core.Mapeadores
{
    public class StockMapeador
    {
        public static StockDTO ToDTO(Stock stock)
        {
            return new StockDTO
            {
                codigoInventario = stock.inventario.codigo,
                codigoProducto = stock.producto.codigo,
                cantidad = stock.cantidad
            };
        }
    }
}