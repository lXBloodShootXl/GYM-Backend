using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IPersonaRepositorio
    {
        Task<PersonaDTO> GetPersona(string ci);
        Task<List<PersonaDTO>> GetPersona();
        Task<List<PersonaDTO>> GetPersonaBorrados();
        Task<PersonaDTO> PostPersona(string ci, string nombre, string? apellido_p, string? apellido_m, bool sexo, string fecha_nacimiento, string hashhuella);
        Task<PersonaDTO> PutPersona(string ci, string? nombre, string? apellido_p, string? apellido_m, bool? sexo, string? fecha_nacimiento, string? hashhuella);
        Task<PersonaDTO> DeletePersona(string ci);
        Task<PersonaDTO?> HabilitarPersona(string ci);
    }
}
