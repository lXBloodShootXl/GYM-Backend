using GYM.Core.DTOs;
using GYM.Core.Models;

namespace GYM.Core.Mapeadores
{
    public class CategoriaMapeador
    {
        public static CategoriaDTO ToDTO(Categoria categoria)
        {
            return new CategoriaDTO
            {
                codigo = categoria.codigo,
                nombre = categoria.nombre,
                descripcion = categoria.descripcion
            };
        }

        public static Categoria ToModel(CategoriaDTO dto)
        {
            return new Categoria
            {
                codigo = dto.codigo,
                nombre = dto.nombre,
                descripcion = dto.descripcion
            };
        }
    }
}