using GYM.Core.DTOs;

namespace GYM.Core.Interfaces
{
    public interface ITurnoRepositorio
    {
        Task<List<TurnoDTO>> GetTurno();

        Task<TurnoDTO?> GetTurnoByCodigo(string codigo);

        Task<TurnoDTO?> PostTurno(
            string codigo,
            string nombre,
            string hora_inicio,
            string hora_fin
        );

        Task<TurnoDTO?> PutTurno(
            string codigo,
            string nombre,
            string hora_inicio,
            string hora_fin
        );

        Task<bool> DeleteTurno(string codigo);
    }
}