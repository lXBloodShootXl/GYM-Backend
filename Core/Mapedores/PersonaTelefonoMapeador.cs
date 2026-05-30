using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class PersonaTelefonoMapeador
    {
        public static PersonaTelefonoDTO toPersonaTelefonoDTO(this PersonaTelefono personatelf)
        {
            return new PersonaTelefonoDTO()
            {
                ci= personatelf.ci,
                telf = personatelf.telf,
                fecha_inicio = personatelf.fecha_inicio,
                fecha_fin = personatelf.fecha_fin
            };
        }
    }
}
