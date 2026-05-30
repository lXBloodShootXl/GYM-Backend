using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IPersonaTelefonoRepositorio
    {
        Task<List<PersonaTelefonoDTO>> GetPersonaTelefono(string ci, string telf);
        Task<List<PersonaTelefonoDTO>> GetPersonaTelefono();
        Task<List<PersonaTelefonoDTO>> GetPersonaTelefonoBorrados();
        Task<PersonaTelefonoDTO> PostPersonaTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin);
        Task<PersonaTelefonoDTO> PutPersonaTelefono(string ci, string telf, string fecha_inicio, string? fecha_fin);
        Task<PersonaTelefonoDTO> DeletePersonaTelefono(string ci, string telf);
        Task<PersonaTelefonoDTO?> HabilitarPersonaTelefono(string ci, string telf);
    }
}
