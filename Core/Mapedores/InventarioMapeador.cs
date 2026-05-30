using GYM.Core.DTOs;
using GYM.Core.Models;

namespace GYM.Core.Mapeadores
{
    public class InventarioMapeador
    {
        public static InventarioDTO ToDTO(Inventario inventario)
        {
            return new InventarioDTO
            {
                codigo = inventario.codigo,
                nombre = inventario.nombre
            };
        }

        public static Inventario ToModel(InventarioDTO dto)
        {
            return new Inventario
            {
                codigo = dto.codigo,
                nombre = dto.nombre
            };
        }
    }
}