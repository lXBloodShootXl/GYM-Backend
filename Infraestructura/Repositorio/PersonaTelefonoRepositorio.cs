using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using GYM.Core.Mapedores;

namespace GYM.Infraestructura.Repositorio
{
    public class PersonaTelefonoRepositorio : IPersonaTelefonoRepositorio
    {
        private readonly GYM_DBContext _context;

        public PersonaTelefonoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaTelefonoDTO>> GetPersonaTelefono(
            string ci,
            string telf)
        {
            return await (
                from pt in _context.PersonaTelefonos.AsNoTracking()
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where
                    p.ci == ci &&
                    t.telf == telf &&
                    pt.estado &&
                    p.estado &&
                    t.estado
                select new PersonaTelefonoDTO
                {
                    ci = p.ci,
                    telf = t.telf,
                    fecha_inicio = pt.fecha_inicio,
                    fecha_fin = pt.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<PersonaTelefonoDTO>> GetPersonaTelefono()
        {
            return await (
                from pt in _context.PersonaTelefonos.AsNoTracking()
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where
                    pt.estado &&
                    p.estado &&
                    t.estado
                select new PersonaTelefonoDTO
                {
                    ci = p.ci,
                    telf = t.telf,
                    fecha_inicio = pt.fecha_inicio,
                    fecha_fin = pt.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<PersonaTelefonoDTO>> GetPersonaTelefonoBorrados()
        {
            return await (
                from pt in _context.PersonaTelefonos.AsNoTracking()
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where !pt.estado
                select new PersonaTelefonoDTO
                {
                    ci = p.ci,
                    telf = t.telf,
                    fecha_inicio = pt.fecha_inicio,
                    fecha_fin = pt.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<PersonaTelefonoDTO?> PostPersonaTelefono(
            string ci,
            string telf,
            string fecha_inicio,
            string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(telf) ||
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

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(x =>
                    x.telf == telf &&
                    x.estado);

            if (telefono == null)
                return null;

            var existe = await _context.PersonaTelefonos
                .AnyAsync(x =>
                    x.id_persona == persona.id_persona &&
                    x.id_telefono == telefono.id_telefono &&
                    x.fecha_inicio == fechaInicio.Value &&
                    x.estado);

            if (existe)
                return null;

            var entity = new PersonaTelefono
            {
                id_persona = persona.id_persona,
                id_telefono = telefono.id_telefono,
                ci = persona.ci,
                telf = telefono.telf,
                fecha_inicio = fechaInicio.Value,
                fecha_fin = fechaFin,
                estado = true
            };

            _context.PersonaTelefonos.Add(entity);

            await _context.SaveChangesAsync();

            return new PersonaTelefonoDTO
            {
                ci = persona.ci,
                telf = telefono.telf,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaTelefonoDTO?> PutPersonaTelefono(
            string ci,
            string telf,
            string fecha_inicio,
            string? fecha_fin)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(telf) ||
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
                from pt in _context.PersonaTelefonos
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where
                    p.ci == ci &&
                    t.telf == telf &&
                    pt.fecha_inicio == fechaInicio.Value &&
                    pt.estado
                select pt
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.fecha_fin = fechaFin;

            await _context.SaveChangesAsync();

            return new PersonaTelefonoDTO
            {
                ci = ci,
                telf = telf,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaTelefonoDTO?> DeletePersonaTelefono(
            string ci,
            string telf)
        {
            var entity = await (
                from pt in _context.PersonaTelefonos
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where
                    p.ci == ci &&
                    t.telf == telf &&
                    pt.estado
                select pt
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = false;

            await _context.SaveChangesAsync();

            return new PersonaTelefonoDTO
            {
                ci = ci,
                telf = telf,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<PersonaTelefonoDTO?> HabilitarPersonaTelefono(
            string ci,
            string telf)
        {
            var entity = await (
                from pt in _context.PersonaTelefonos
                join p in _context.Personas
                    on pt.id_persona equals p.id_persona
                join t in _context.Telefonos
                    on pt.id_telefono equals t.id_telefono
                where
                    p.ci == ci &&
                    t.telf == telf &&
                    !pt.estado
                select pt
            ).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            entity.estado = true;

            await _context.SaveChangesAsync();

            return new PersonaTelefonoDTO
            {
                ci = ci,
                telf = telf,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }
    }
}