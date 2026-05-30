using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class PersonaAsistenciaMapeador
    {
        public static PersonaAsistenciaDTO toPersonaAsistenciaDTO(this PersonaAsistencia personaasistencia, string ci, DateOnly fecha)
        {
            return new PersonaAsistenciaDTO()
            {
                ci = ci,
                fecha = fecha
            };
        }
    }
}
