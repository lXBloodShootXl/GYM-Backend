using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;

namespace GYM.Infraestructura.Repositorio
{
    public class TurnoRepositorio : ITurnoRepositorio
    {
        private readonly GYM_DBContext _context;

        public TurnoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<TurnoDTO>> GetTurno()
        {
            return await _context.Turnos
                .AsNoTracking()
                .Where(t => t.estado != false)
                .Select(t => t.toTurnoDTO())
                .ToListAsync();
        }

        public async Task<TurnoDTO?> GetTurnoByCodigo(string codigo)
        {
            var turno = await _context.Turnos
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.codigo == codigo &&
                    t.estado != false);

            if (turno == null)
                return null;

            return turno.toTurnoDTO();
        }


        public async Task<TurnoDTO?> PostTurno(
            string codigo,
            string nombre,
            string hora_inicio,
            string hora_fin)
        {
            if (
                string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(nombre)
            )
                return null;

            bool existe = await _context.Turnos
                .AnyAsync(t =>
                    t.codigo == codigo &&
                    t.estado != false);

            if (existe)
                return null;

            var turno = new Turno
            {
                codigo = codigo,
                nombre = nombre,
                hora_inicio = hora_inicio,
                hora_fin = hora_fin,
                estado = true
            };

            _context.Turnos.Add(turno);

            await _context.SaveChangesAsync();

            return turno.toTurnoDTO();
        }

        public async Task<TurnoDTO?> PutTurno(
    string codigo,
    string nombre,
    string hora_inicio,
    string hora_fin)
        {
            var turno = await _context.Turnos
                .FirstOrDefaultAsync(t =>
                    t.codigo == codigo &&
                    t.estado != false);

            if (turno == null)
                return null;

            turno.nombre = nombre;
            turno.hora_inicio = hora_inicio;
            turno.hora_fin = hora_fin;

            await _context.SaveChangesAsync();

            return turno.toTurnoDTO();
        }

        public async Task<bool> DeleteTurno(string codigo)
        {
            var turno = await _context.Turnos
                .FirstOrDefaultAsync(t =>
                    t.codigo == codigo &&
                    t.estado != false);

            if (turno == null)
                return false;

            turno.estado = false;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}