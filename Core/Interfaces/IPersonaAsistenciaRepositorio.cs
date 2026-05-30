using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IPersonaAsistenciaRepositorio
    {
        Task<List<PersonaAsistenciaDTO>> GetPersonaAsistencia(string ci, string fecha);
        Task<List<PersonaAsistenciaDTO>> GetPersonaAsistencia();
        Task<List<PersonaAsistenciaDTO>> GetPersonaAsistenciaBorrados();
        Task<PersonaAsistenciaDTO> PostPersonaAsistencia(string ci, string fecha);
        Task<PersonaAsistenciaDTO> PostPersonaAsistenciaHuella(string huella, string fecha);
        Task<PersonaAsistenciaDTO> DeletePersonaAsistencia(string ci, string fecha);
        Task<PersonaAsistenciaDTO?> HabilitarPersonaAsistencia(string ci, string fecha);
    }
}
