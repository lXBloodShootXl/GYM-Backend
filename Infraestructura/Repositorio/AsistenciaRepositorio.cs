using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GYM.Infraestructura.Repositorio
{
    public class AsistenciaRepositorio : IAsistenciaRepositorio
    {
        private readonly GYM_DBContext _context;

        public AsistenciaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<AsistenciaDTO?> GetAsistencia(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return null;
            var f = fecha.toDateOnly();
            if (f == null)
            {
                return null;
            }
            else
            {
                return await _context.Asistencias
                    .AsNoTracking()
                    .Where(p => p.fecha == f.Value && p.estado != false)
                    .Select(p => p.toAsistenciaDTO())
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<AsistenciaDTO>> GetAsistencia()
        {
            return await _context.Asistencias
                .AsNoTracking()
                .Where(p => p.estado != false)
                .Select(p => p.toAsistenciaDTO())
                .ToListAsync();
        }

        public async Task<List<AsistenciaDTO>> GetAsistenciaBorrados()
        {
            return await _context.Asistencias
                .AsNoTracking()
                .Where(p => p.estado == false)
                .Select(p => p.toAsistenciaDTO())
                .ToListAsync();
        }

        public async Task<AsistenciaDTO?> PostAsistencia(string? fecha)
        {
            DateOnly fechaFinal;

            if (string.IsNullOrWhiteSpace(fecha))
            {
                fechaFinal = DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                var f = fecha.toDateOnly();

                if (f == null)
                    return null;

                fechaFinal = f.Value;
            }

            var asistencia = new Asistencia
            {
                fecha = fechaFinal,
                estado = true
            };

            _context.Asistencias.Add(asistencia);

            await _context.SaveChangesAsync();

            return asistencia.toAsistenciaDTO();
        }

        public async Task<AsistenciaDTO?> PutAsistencia(string fecha, string fecha_nuevo)
        {
            if (string.IsNullOrWhiteSpace(fecha) || string.IsNullOrWhiteSpace(fecha_nuevo))
                return null;
            var f = fecha.toDateOnly();
            if (f == null)
                return null;
            var f2 = fecha_nuevo.toDateOnly();
            if (f2 == null)
                return null;
            var Asistencia = await _context.Asistencias.FirstOrDefaultAsync(p => p.fecha == f.Value && p.estado != false);
            if (Asistencia == null)
                return null;
            Asistencia.fecha = f2.Value;
            await _context.SaveChangesAsync();
            return Asistencia.toAsistenciaDTO();
        }

        public async Task<AsistenciaDTO?> DeleteAsistencia(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;
            var Asistencia = await _context.Asistencias.FirstOrDefaultAsync(p => p.fecha == f.Value && p.estado == true);
            if (Asistencia == null) return null;
            Asistencia.estado = false;
            await _context.SaveChangesAsync();
            return Asistencia.toAsistenciaDTO();
        }

        public async Task<AsistenciaDTO?> HabilitarAsistencia(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;
            var Asistencia = await _context.Asistencias.FirstOrDefaultAsync(p => p.fecha == f.Value && p.estado == false);
            if (Asistencia == null) return null;
            Asistencia.estado = true;
            await _context.SaveChangesAsync();
            return Asistencia.toAsistenciaDTO();
        }
    }
}
