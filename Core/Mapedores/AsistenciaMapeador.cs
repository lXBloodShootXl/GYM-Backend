using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class AsistenciaMapeador
    {
        public static AsistenciaDTO toAsistenciaDTO(this Asistencia asistencia)
        {
            return new AsistenciaDTO()
            {
                fecha = asistencia.fecha
            };
        }
    }
}
