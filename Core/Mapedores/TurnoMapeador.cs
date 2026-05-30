using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores

{
    public static class TurnoMapeador
    {
        public static TurnoDTO toTurnoDTO(this Turno turno)
        {
            return new TurnoDTO
            {
                codigo = turno.codigo,
                nombre = turno.nombre,
                hora_inicio = turno.hora_inicio,
                hora_fin = turno.hora_fin
            };
        }
    }
}