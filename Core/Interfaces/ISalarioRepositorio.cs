using DTOS;

namespace Interfaces
{
    public interface ISalarioRepositorio
    {
        Task<List<SalarioDTO>> GetSalarios();
        Task<SalarioDTO> PostSalario(string codigo, int salario);
        Task<SalarioDTO> PatchSalario(string codigo, int salario);
        Task<SalarioDTO> DeleteSalario(string codigo);
    }
}