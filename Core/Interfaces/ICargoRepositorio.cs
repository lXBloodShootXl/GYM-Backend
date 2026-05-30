using DTOS;

namespace Interfaces
{
    public interface ICargoRepositorio
    {
        Task<List<CargoDTO>> GetCargos();
        Task<CargoDTO> POSTCargos(string codigo, string nombre);
        Task<CargoDTO> PatchCargos(string codigo, string nombre);
        Task<CargoDTO> DeleteCargos(string codigo);
    }
}