using DTOS;

namespace Interfaces
{
    public interface IEmpleadoCargoRepositorio
    {
        Task<List<EmpleadoCargoDTO>> GetEmpleadoCargoSalario();
        Task<EmpleadoCargoDTO> POSTEmpleadoCargoSalario(string codigoEmpleado,string codigoCargo ,string FechaInicio,string FechaFin);
        Task<EmpleadoCargoDTO>PUTEmpleadoCargoSalario(string codigoEmpleado,string codigoCargo ,string FechaInicio,string FechaFin);
        Task<EmpleadoCargoDTO>DeshabilitarCargo(string codigoEmpleado,string codigoCargo);
    }
}