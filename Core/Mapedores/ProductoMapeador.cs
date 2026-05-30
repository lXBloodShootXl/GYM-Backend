using GYM.Core.DTOs;
using GYM.Core.Models;

namespace GYM.Core.Mapeadores
{
    public class ProductoMapeador
    {
        public static ProductoDTO ToDTO(Producto producto)
        {
            return new ProductoDTO
            {
                codigo = producto.codigo,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio = producto.precio,
                codigoCategoria = producto.Categoria.codigo
            };
        }

        public static Producto ToModel(ProductoDTO dto)
        {
            return new Producto
            {
                codigo = dto.codigo,
                nombre = dto.nombre,
                descripcion = dto.descripcion,
                precio = dto.precio
            };
        }
    }
}