using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IPersonaCorreoRepositorio
    {
        Task<List<PersonaCorreoDTO>> GetPersonaCorreo(string ci, string correo);
        Task<List<PersonaCorreoDTO>> GetPersonaCorreo();
        Task<List<PersonaCorreoDTO>> GetPersonaCorreoBorrados();
        Task<PersonaCorreoDTO> PostPersonaCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin);
        Task<PersonaCorreoDTO> PutPersonaCorreo(string ci, string correo, string fecha_inicio, string? fecha_fin);
        Task<PersonaCorreoDTO> DeletePersonaCorreo(string ci, string correo);
        Task<PersonaCorreoDTO?> HabilitarPersonaCorreo(string ci, string correo);
    }
}
