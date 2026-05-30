using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class CorreoMapeador
    {
        public static CorreoDTO toCorreoDTO(this Correo Correo)
        {
            return new CorreoDTO()
            {
                correo = Correo.correo
            };
        }
    }
}
