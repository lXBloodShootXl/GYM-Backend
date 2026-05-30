using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface IAsistenciaRepositorio
    {
        Task<AsistenciaDTO> GetAsistencia(string fecha);
        Task<List<AsistenciaDTO>> GetAsistencia();
        Task<List<AsistenciaDTO>> GetAsistenciaBorrados();
        Task<AsistenciaDTO> PostAsistencia(string fecha);
        Task<AsistenciaDTO> DeleteAsistencia(string fecha);
        Task<AsistenciaDTO?> HabilitarAsistencia(string fecha);
    }
}
