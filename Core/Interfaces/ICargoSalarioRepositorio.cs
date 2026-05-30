using DTOS;

namespace Interfaces
{
    public interface ICargoSalarioRepositorio
    {
        Task<List<CargoSalarioDTO>> GetCargoSalario();
        Task<CargoSalarioDTO> POSTCargoSalario(string codigoSalario,string codigoCArgo , string  FechaInicio,string FechaFin);
         Task<CargoSalarioDTO>PutCargoSalario(string codigoSalario, string CodigoCargo, string FechaInicio, string FechaFin);
        
    }
}