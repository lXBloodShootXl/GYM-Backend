using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class TelefonoMapeador
    {
        public static TelefonoDTO toTelefonoDTO(this Telefono telefono)
        {
            return new TelefonoDTO()
            {
                telf = telefono.telf
            };
        }
    }
}
