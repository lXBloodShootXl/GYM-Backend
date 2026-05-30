using GYM.Core.DTOs;
using GYM.Core.Models;

public static class EmpleadoTurnoMapeador
{
    public static EmpleadoTurnoDTO toEmpleadoTurnoDTO(this EmpleadoTurno empleadoTurno)
    {
        return new EmpleadoTurnoDTO()
        {
            ci = empleadoTurno.Empleado.persona.ci,
            nombre = empleadoTurno.Empleado.persona.nombre,
            apellido_p = empleadoTurno.Empleado.persona.apellido_p,
            apellido_m = empleadoTurno.Empleado.persona.apellido_m,

            codigo = empleadoTurno.Turno.codigo,
            nombreTurno = empleadoTurno.Turno.nombre,
            hora_inicio = empleadoTurno.Turno.hora_inicio,
            hora_fin = empleadoTurno.Turno.hora_fin
        };
    }
}