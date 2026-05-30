using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using GYM.Core.Mapedores;

namespace GYM.Infraestructura.Repositorio
{
    public class PersonaCorreoRepositorio : IPersonaCorreoRepositorio
    {
        private readonly GYM_DBContext _context;

        public PersonaCorreoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaCorreoDTO>> GetPersonaCorreo(
            string ci,
            string correo)
        {
            return await (
                from pc in _context.PersonaCorreos.AsNoTracking()
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where
                    p.ci == ci &&
                    c.correo == correo &&
                    pc.estado &&
                    p.estado &&
                    c.estado
                select new PersonaCorreoDTO
                {
                    ci = p.ci,
                    correo = c.correo,
                    fecha_inicio = pc.fecha_inicio,
                    fecha_fin = pc.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<PersonaCorreoDTO>> GetPersonaCorreo()
        {
            return await (
                from pc in _context.PersonaCorreos.AsNoTracking()
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where
                    pc.estado &&
                    p.estado &&
                    c.estado
                select new PersonaCorreoDTO
                {
                    ci = p.ci,
                    correo = c.correo,
                    fecha_inicio = pc.fecha_inicio,
                    fecha_fin = pc.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<PersonaCorreoDTO>> GetPersonaCorreoBorrados()
        {
            return await (
                from pc in _context.PersonaCorreos.AsNoTracking()
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where !pc.estado
                select new PersonaCorreoDTO
                {
                    ci = p.ci,
                    correo = c.correo,
                    fecha_inicio = pc.fecha_inicio,
                    fecha_fin = pc.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<PersonaCorreoDTO?> PostPersonaCorreo(
            string ci,
            string correo,
            string fecha_inicio,
            string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(fecha_inicio))
                return null;

            var fechaInicio = fecha_inicio.toDateOnly();

            if (fechaInicio == null)
                return null;

            DateOnly? fechaFin = null;

            if (!string.IsNullOrWhiteSpace(fecha_fin))
            {
                fechaFin = fecha_fin.toDateOnly();

                if (fechaFin == null)
                    return null;
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(x =>
                    x.ci == ci &&
                    x.estado);

            if (persona == null)
                return null;

            var correoEntity = await _context.Correos
                .FirstOrDefaultAsync(x =>
                    x.correo == correo &&
                    x.estado);

            if (correoEntity == null)
                return null;

            var existe = await _context.PersonaCorreos
                .AnyAsync(x =>
                    x.id_persona == persona.id_persona &&
                    x.id_correo == correoEntity.id_correo &&
                    x.fecha_inicio == fechaInicio.Value &&
                    x.estado);

            if (existe)
                return null;

            var entity = new PersonaCorreo
            {
                id_persona = persona.id_persona,
                id_correo = correoEntity.id_correo,
                ci = persona.ci,
                correo = correoEntity.correo,
                fecha_inicio = fechaInicio.Value,
                fecha_fin = fechaFin,
                estado = true
            };

            _context.PersonaCorreos.Add(entity);

            await _context.SaveChangesAsync();

            return new PersonaCorreoDTO
            {
                ci = persona.ci,
                correo = correoEntity.correo,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaCorreoDTO?> PutPersonaCorreo(
            string ci,
            string correo,
            string fecha_inicio,
            string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(fecha_inicio))
                return null;

            var fechaInicio = fecha_inicio.toDateOnly();

            if (fechaInicio == null)
                return null;

            DateOnly? fechaFin = null;

            if (!string.IsNullOrWhiteSpace(fecha_fin))
            {
                fechaFin = fecha_fin.toDateOnly();

                if (fechaFin == null)
                    return null;
            }

            var entity = await (
                from pc in _context.PersonaCorreos
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where
                    p.ci == ci &&
                    c.correo == correo &&
                    pc.fecha_inicio == fechaInicio.Value &&
                    pc.estado
                select pc
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.fecha_fin = fechaFin;

            await _context.SaveChangesAsync();

            return new PersonaCorreoDTO
            {
                ci = ci,
                correo = correo,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaCorreoDTO?> DeletePersonaCorreo(
            string ci,
            string correo)
        {
            var entity = await (
                from pc in _context.PersonaCorreos
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where
                    p.ci == ci &&
                    c.correo == correo &&
                    pc.estado
                select pc
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = false;

            await _context.SaveChangesAsync();

            return new PersonaCorreoDTO
            {
                ci = ci,
                correo = correo,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaCorreoDTO?> HabilitarPersonaCorreo(
            string ci,
            string correo)
        {
            var entity = await (
                from pc in _context.PersonaCorreos
                join p in _context.Personas
                    on pc.id_persona equals p.id_persona
                join c in _context.Correos
                    on pc.id_correo equals c.id_correo
                where
                    p.ci == ci &&
                    c.correo == correo &&
                    !pc.estado
                select pc
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = true;

            await _context.SaveChangesAsync();

            return new PersonaCorreoDTO
            {
                ci = ci,
                correo = correo,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }
    }
}