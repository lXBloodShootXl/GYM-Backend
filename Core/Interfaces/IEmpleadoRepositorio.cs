using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IEmpleadoRepositorio
    {
        Task<List<EmpleadoDTO>> GetEmpleado();
        Task<EmpleadoDTO?> PostEmpleado(string ci, string pwd);
        Task<EmpleadoDTO?> PutEmpleado(string ci, string pwd, string nuevo_pwd);
        Task<bool> DeleteEmpleado(string ci);
        Task<EmpleadoDTO?> PutEmpleado(string ci, DateOnly fecha);
        Task<bool> LoginEmpleado(string ci, string pwd);
    }
}
