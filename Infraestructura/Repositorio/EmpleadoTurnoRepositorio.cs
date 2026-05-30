using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Models;

namespace GYM.Infraestructura.Repositorio
{
    public class EmpleadoTurnoRepositorio : IEmpleadoTurnoRepositorio
    {
        private readonly GYM_DBContext _context;

        public EmpleadoTurnoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoTurnoDTO>> GetEmpleadoTurno()
        {
            return await _context.EmpleadoTurnos
                .Include(et => et.Empleado)
                    .ThenInclude(e => e.persona)
                .Include(et => et.Turno)
                .AsNoTracking()
                .Select(et => et.toEmpleadoTurnoDTO())
                .ToListAsync();
        }

        public async Task<EmpleadoTurnoDTO?> PostEmpleadoTurno(string ci, string codigo)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e => e.persona.ci == ci);

            if (empleado == null)
                return null;

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(t => t.codigo == codigo);

            if (turno == null)
                return null;

            var empleadoTurno = new EmpleadoTurno
            {
                id_empleado = empleado.id_empleado,
                id_turno = turno.id_turno
            };

            _context.EmpleadoTurnos.Add(empleadoTurno);
            await _context.SaveChangesAsync();

            empleadoTurno.Empleado = empleado;
            empleadoTurno.Turno = turno;

            return empleadoTurno.toEmpleadoTurnoDTO();
        }

        public async Task<List<EmpleadoTurnoDTO>> GetTurnosPorEmpleado(string ci)
        {
            return await _context.EmpleadoTurnos
                .Include(et => et.Empleado)
                    .ThenInclude(e => e.persona)
                .Include(et => et.Turno)
                .Where(et => et.Empleado.persona.ci == ci)
                .AsNoTracking()
                .Select(et => et.toEmpleadoTurnoDTO())
                .ToListAsync();
        }

        public async Task<List<EmpleadoTurnoDTO>> GetEmpleadosPorTurno(string codigo)
        {
            return await _context.EmpleadoTurnos
                .Include(et => et.Empleado)
                    .ThenInclude(e => e.persona)
                .Include(et => et.Turno)
                .Where(et => et.Turno.codigo == codigo)
                .AsNoTracking()
                .Select(et => et.toEmpleadoTurnoDTO())
                .ToListAsync();
        }




        public async Task<EmpleadoTurnoDTO?> PutEmpleadoTurno(
            string ci,
            string codigoAnterior,
            string codigoNuevo)
        {
            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e => e.persona.ci == ci);

            var turnoAnterior = await _context.Turnos
                .FirstOrDefaultAsync(t => t.codigo == codigoAnterior);

            var turnoNuevo = await _context.Turnos
                .FirstOrDefaultAsync(t => t.codigo == codigoNuevo);

            if (empleado == null ||
                turnoAnterior == null ||
                turnoNuevo == null)
            {
                return null;
            }

            var relacionExistente = await _context.EmpleadoTurnos
                .FirstOrDefaultAsync(et =>
                    et.id_empleado == empleado.id_empleado &&
                    et.id_turno == turnoAnterior.id_turno);

            if (relacionExistente == null)
                return null;

            _context.EmpleadoTurnos.Remove(relacionExistente);

            var nuevaRelacion = new EmpleadoTurno
            {
                id_empleado = empleado.id_empleado,
                id_turno = turnoNuevo.id_turno
            };

            _context.EmpleadoTurnos.Add(nuevaRelacion);

            await _context.SaveChangesAsync();

            nuevaRelacion.Empleado = empleado;
            nuevaRelacion.Turno = turnoNuevo;

            return nuevaRelacion.toEmpleadoTurnoDTO();
        }

        public async Task<bool> DeleteEmpleadoTurno(string ci, string codigo)
        {
            var relacion = await _context.EmpleadoTurnos
                .Include(et => et.Empleado)
                    .ThenInclude(e => e.persona)
                .Include(et => et.Turno)
                .FirstOrDefaultAsync(et =>
                    et.Empleado.persona.ci == ci &&
                    et.Turno.codigo == codigo);

            if (relacion == null)
                return false;

            _context.EmpleadoTurnos.Remove(relacion);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}