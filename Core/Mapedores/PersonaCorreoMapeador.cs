using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class PersonaCorreoMapeador
    {
        public static PersonaCorreoDTO toPersonaCorreoDTO(this PersonaCorreo personacorreo)
        {
            return new PersonaCorreoDTO()
            {
                ci= personacorreo.ci,
                correo = personacorreo.correo,
                fecha_inicio = personacorreo.fecha_inicio,
                fecha_fin = personacorreo.fecha_fin
            };
        }
    }
}
