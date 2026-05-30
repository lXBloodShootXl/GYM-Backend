using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IEmpleadoTurnoRepositorio
    {
        Task<List<EmpleadoTurnoDTO>> GetEmpleadoTurno();
        Task<EmpleadoTurnoDTO?> PostEmpleadoTurno(string ci, string codigo);
        Task<List<EmpleadoTurnoDTO>> GetTurnosPorEmpleado(string ci);
        Task<List<EmpleadoTurnoDTO>> GetEmpleadosPorTurno(string codigo);
        Task<EmpleadoTurnoDTO?> PutEmpleadoTurno(string ci, string codigoAnterior, string codigoNuevo);
        Task<bool> DeleteEmpleadoTurno(string ci, string codigo);
    }
}