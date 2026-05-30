using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface ISuscripcionRepositorio
    {
        Task<List<SuscripcionDTO>> GetSuscripcion(string ci);
        Task<List<SuscripcionDTO>> GetSuscripcionMembresia(string ci, string codigo);
        Task<List<SuscripcionDTO>> GetSuscripcion();
        Task<List<SuscripcionDTO>> GetSuscripcionBorrados();
        Task<SuscripcionDTO> PostSuscripcion(string ci, string codigo, string fecha_inicio, string fecha_fin);
        Task<SuscripcionDTO> PutSuscripcion(string ci, string codigo, string fecha_inicio, string fecha_fin);
        Task<SuscripcionDTO> DeleteSuscripcion(string ci, string codigo);
        Task<SuscripcionDTO?> HabilitarSuscripcion(string ci, string codigo);
    }
}
