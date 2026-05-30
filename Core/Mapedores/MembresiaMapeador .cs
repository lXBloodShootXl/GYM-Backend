using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class MembresiaMapeador
    {
        public static MembresiaDTO toMembresiaDTO(this Membresia Membresia)
        {
            return new MembresiaDTO()
            {
                codigo = Membresia.codigo,
                nombre = Membresia.nombre,
                duracion = Membresia.duracion,
                precio = Membresia.precio
            };
        }
    }
}
