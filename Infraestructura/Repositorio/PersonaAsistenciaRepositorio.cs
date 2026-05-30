using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using GYM.Core.Mapedores;

namespace GYM.Infraestructura.Repositorio
{
    public class PersonaAsistenciaRepositorio : IPersonaAsistenciaRepositorio
    {
        private readonly GYM_DBContext _context;

        public PersonaAsistenciaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaAsistenciaDTO>?> GetPersonaAsistencia(
            string ci,
            string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;

            var personaAsistencias = await (
                from pa in _context.PersonaAsistencias.AsNoTracking()
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where
                    p.ci == ci &&
                    a.fecha == f.Value &&
                    pa.estado &&
                    p.estado &&
                    a.estado
                select new PersonaAsistenciaDTO
                {
                    ci = p.ci,
                    fecha = a.fecha
                }
            ).ToListAsync();

            return personaAsistencias;
        }

        public async Task<List<PersonaAsistenciaDTO>> GetPersonaAsistencia()
        {
            return await (
                from pa in _context.PersonaAsistencias.AsNoTracking()
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where pa.estado &&
                      p.estado &&
                      a.estado
                select new PersonaAsistenciaDTO
                {
                    ci = p.ci,
                    fecha = a.fecha
                }
            ).ToListAsync();
        }

        public async Task<List<PersonaAsistenciaDTO>> GetPersonaAsistenciaBorrados()
        {
            return await (
                from pa in _context.PersonaAsistencias.AsNoTracking()
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where !pa.estado
                select new PersonaAsistenciaDTO
                {
                    ci = p.ci,
                    fecha = a.fecha
                }
            ).ToListAsync();
        }

        public async Task<PersonaAsistenciaDTO?> PostPersonaAsistencia(
            string ci,
            string? fecha)
        {
            if (string.IsNullOrWhiteSpace(ci))
                return null;
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

            var persona = await _context.Personas
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.estado);

            if (persona == null)
                return null;

            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(x =>
                    x.fecha == fechaFinal &&
                    x.estado);

            if (asistencia == null)
                return null;

            var existe = await _context.PersonaAsistencias
                .AnyAsync(x =>
                    x.id_persona == persona.id_persona &&
                    x.id_asistencia == asistencia.id_asistencia &&
                    x.estado);

            if (existe)
                return null;

            var entity = new PersonaAsistencia
            {
                id_persona = persona.id_persona,
                id_asistencia = asistencia.id_asistencia,
                estado = true
            };

            _context.PersonaAsistencias.Add(entity);

            await _context.SaveChangesAsync();

            return new PersonaAsistenciaDTO
            {
                ci = persona.ci,
                fecha = asistencia.fecha
            };
        }
        public async Task<PersonaAsistenciaDTO?> PostPersonaAsistenciaHuella(
    string huella,
    string? fecha)
        {
            if (string.IsNullOrWhiteSpace(huella))
                return null;

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

            var persona = await _context.Personas
                .FirstOrDefaultAsync(x =>
                    x.hashhuella == huella &&
                    x.estado);

            if (persona == null)
                return null;

            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(x =>
                    x.fecha == fechaFinal &&
                    x.estado);

            if (asistencia == null)
            {
                asistencia = new Asistencia
                {
                    fecha = fechaFinal,
                    estado = true
                };

                _context.Asistencias.Add(asistencia);

                await _context.SaveChangesAsync();
            }

            var existe = await _context.PersonaAsistencias
                .AnyAsync(x =>
                    x.id_persona == persona.id_persona &&
                    x.id_asistencia == asistencia.id_asistencia &&
                    x.estado);

            if (existe)
                return null;

            var entity = new PersonaAsistencia
            {
                id_persona = persona.id_persona,
                id_asistencia = asistencia.id_asistencia,
                estado = true
            };

            _context.PersonaAsistencias.Add(entity);

            await _context.SaveChangesAsync();

            return new PersonaAsistenciaDTO
            {
                ci = persona.ci,
                fecha = asistencia.fecha
            };
        }

        public async Task<PersonaAsistenciaDTO?> PutPersonaAsistencia(
            string ci,
            string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;

            var entity = await (
                from pa in _context.PersonaAsistencias
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where
                    p.ci == ci &&
                    a.fecha == f.Value &&
                    pa.estado
                select pa
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = true;

            await _context.SaveChangesAsync();

            return new PersonaAsistenciaDTO
            {
                ci = ci,
                fecha = f.Value
            };
        }

        public async Task<PersonaAsistenciaDTO?> DeletePersonaAsistencia(
            string ci,
            string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;

            var entity = await (
                from pa in _context.PersonaAsistencias
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where
                    p.ci == ci &&
                    a.fecha == f.Value &&
                    pa.estado
                select pa
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = false;

            await _context.SaveChangesAsync();

            return new PersonaAsistenciaDTO
            {
                ci = ci,
                fecha = f.Value
            };
        }

        public async Task<PersonaAsistenciaDTO?> HabilitarPersonaAsistencia(
            string ci,
            string fecha)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(fecha))
                return null;

            var f = fecha.toDateOnly();

            if (f == null)
                return null;

            var entity = await (
                from pa in _context.PersonaAsistencias
                join p in _context.Personas
                    on pa.id_persona equals p.id_persona
                join a in _context.Asistencias
                    on pa.id_asistencia equals a.id_asistencia
                where
                    p.ci == ci &&
                    a.fecha == f.Value &&
                    !pa.estado
                select pa
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = true;

            await _context.SaveChangesAsync();

            return new PersonaAsistenciaDTO
            {
                ci = ci,
                fecha = f.Value
            };
        }
    }
}