using GYM.Core.DTOs;
using GYM.Core.Models;
using Models;

namespace GYM.Core.Mapedores
{
    public static class EmpleadoMapeador
    {
        public static EmpleadoDTO toEmpleadoDTO(this Empleado empleado)
        {
            return new EmpleadoDTO()
            {
                nombre = empleado.persona.nombre,
                ci = empleado.persona.ci,
                apellido_p = empleado.persona.apellido_p,
                apellido_m = empleado.persona.apellido_m,
                fecha = empleado.fecha
            };
        }
    }
}
