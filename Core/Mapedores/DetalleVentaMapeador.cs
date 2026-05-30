using GYM.Core.DTOs;
using GYM.Core.Models;


namespace GYM.Core.Mapeadores
{
    public class DetalleVentaMapeador
    {
        public static DetalleVentaDTO ToDTO(DetalleVenta detalle)
        {
            return new DetalleVentaDTO
            {
                codigoVenta = detalle.ventas.codigo,
                codigoProducto = detalle.producto.codigo,
                cantidad = detalle.cantidad
            };
        }

        public static DetalleVenta ToModel(DetalleVentaDTO dto)
        {
            return new DetalleVenta
            {
                cantidad = dto.cantidad
            };
        }
    }
}