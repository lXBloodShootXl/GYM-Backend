using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IMembresiaRepositorio
    {
        Task<MembresiaDTO> GetMembresia(string codigo);
        Task<List<MembresiaDTO>> GetMembresia();
        Task<List<MembresiaDTO>> GetMembresiaBorrados();
        Task<MembresiaDTO> PostMembresia(string codigo, string nombre, int duracion, decimal precio);
        Task<MembresiaDTO> PutMembresia(string codigo, string? nombre, int? duracion, decimal? precio);
        Task<MembresiaDTO> DeleteMembresia(string codigo);
        Task<MembresiaDTO?> HabilitarMembresia(string codigo);
    }
}