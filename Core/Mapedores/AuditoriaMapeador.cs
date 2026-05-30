using GYM.Core.DTOs;
using GYM.Core.Models;

namespace GYM.Core.Mapeadores
{
    public class AuditoriaMapeador
    {
        public static AuditoriaDTO ToDTO(Auditoria auditoria)
        {
            return new AuditoriaDTO
            {
                tabla = auditoria.tabla,
                id_registro = auditoria.id_registro,
                accion = auditoria.accion,
                datos_anteriores = auditoria.datos_anteriores,
                datos_nuevos = auditoria.datos_nuevos
            };
        }

        public static Auditoria ToModel(AuditoriaDTO dto)
        {
            return new Auditoria
            {
                tabla = dto.tabla,
                id_registro = dto.id_registro,
                accion = dto.accion,
                datos_anteriores = dto.datos_anteriores,
                datos_nuevos = dto.datos_nuevos
            };
        }
    }
}