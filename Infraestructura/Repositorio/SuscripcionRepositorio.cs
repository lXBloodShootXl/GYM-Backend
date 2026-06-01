using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using GYM.Core.Mapedores;

namespace GYM.Infraestructura.Repositorio
{
    public class SuscripcionRepositorio : ISuscripcionRepositorio
    {
        private readonly GYM_DBContext _context;

        public SuscripcionRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<SuscripcionDTO>> GetSuscripcion(string ci)
        {
            return await (
                from s in _context.Suscripciones.AsNoTracking()
                join c in _context.Clientes.AsNoTracking()
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias.AsNoTracking()
                    on s.id_membresia equals m.id_membresia
                where
                    p.ci == ci &&
                    s.estado &&
                    c.estado &&
                    p.estado &&
                    m.estado
                select new SuscripcionDTO
                {
                    ci = p.ci,
                    codigo = m.codigo,
                    nombre = m.nombre,
                    fecha_inicio = s.fecha_inicio,
                    fecha_fin = s.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<SuscripcionDTO>> GetSuscripcionMembresia(
            string ci,
            string codigo)
        {
            return await (
                from s in _context.Suscripciones.AsNoTracking()
                join c in _context.Clientes.AsNoTracking()
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias.AsNoTracking()
                    on s.id_membresia equals m.id_membresia
                where
                    p.ci == ci &&
                    m.codigo == codigo &&
                    s.estado &&
                    c.estado &&
                    p.estado &&
                    m.estado
                select new SuscripcionDTO
                {
                    ci = p.ci,
                    codigo = m.codigo,
                    nombre = m.nombre,
                    fecha_inicio = s.fecha_inicio,
                    fecha_fin = s.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<SuscripcionDTO>> GetSuscripcion()
        {
            return await (
                from s in _context.Suscripciones.AsNoTracking()
                join c in _context.Clientes.AsNoTracking()
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias.AsNoTracking()
                    on s.id_membresia equals m.id_membresia
                where
                    s.estado &&
                    c.estado &&
                    p.estado &&
                    m.estado
                select new SuscripcionDTO
                {
                    ci = p.ci,
                    codigo = m.codigo,
                    nombre = m.nombre,
                    fecha_inicio = s.fecha_inicio,
                    fecha_fin = s.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<List<SuscripcionDTO>> GetSuscripcionBorrados()
        {
            return await (
                from s in _context.Suscripciones.AsNoTracking()
                join c in _context.Clientes.AsNoTracking()
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias.AsNoTracking()
                    on s.id_membresia equals m.id_membresia
                where !s.estado
                select new SuscripcionDTO
                {
                    ci = p.ci,
                    codigo = m.codigo,
                    nombre = m.nombre,
                    fecha_inicio = s.fecha_inicio,
                    fecha_fin = s.fecha_fin
                }
            ).ToListAsync();
        }

        public async Task<SuscripcionDTO?> PostSuscripcion(
            string ci,
            string codigo,
            string fecha_inicio,
            string fecha_fin)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(fecha_inicio) ||
                string.IsNullOrWhiteSpace(fecha_fin)
            )
                return null;

            var fechaInicio = fecha_inicio.toDateOnly();
            var fechaFin = fecha_fin.toDateOnly();

            if (fechaInicio == null || fechaFin == null)
                return null;

            if (fechaFin.Value < fechaInicio.Value)
                return null;

            var cliente = await (
                from c in _context.Clientes.AsNoTracking()
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                where
                    p.ci == ci &&
                    c.estado &&
                    p.estado
                select c
            ).FirstOrDefaultAsync();

            if (cliente == null)
                return null;

            var membresia = await _context.Membresias
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.codigo == codigo &&
                    x.estado);

            if (membresia == null)
                return null;

            var existe = await _context.Suscripciones
                .AnyAsync(x =>
                    x.id_cliente == cliente.id_cliente &&
                    x.id_membresia == membresia.id_membresia &&
                    x.fecha_inicio == fechaInicio.Value);

            if (existe)
                return null;

            var entity = new Suscripcion
            {
                id_cliente = cliente.id_cliente,
                id_membresia = membresia.id_membresia,
                fecha_inicio = fechaInicio.Value,
                fecha_fin = fechaFin.Value,
                estado = true
            };

            _context.Suscripciones.Add(entity);

            await _context.SaveChangesAsync();

            return new SuscripcionDTO
            {
                ci = ci,
                codigo = membresia.codigo,
                nombre = membresia.nombre,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin
            };
        }

        public async Task<SuscripcionDTO?> PutSuscripcion(
            string ci,
            string codigo,
            string fecha_inicio,
            string fecha_fin)
        {
            if (
                string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(fecha_inicio) ||
                string.IsNullOrWhiteSpace(fecha_fin)
            )
                return null;

            var fechaInicio = fecha_inicio.toDateOnly();
            var fechaFin = fecha_fin.toDateOnly();

            if (fechaInicio == null || fechaFin == null)
                return null;

            if (fechaFin.Value < fechaInicio.Value)
                return null;

            var data = await (
                from s in _context.Suscripciones
                join c in _context.Clientes
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias
                    on s.id_membresia equals m.id_membresia
                where
                    p.ci == ci &&
                    m.codigo == codigo &&
                    s.fecha_inicio == fechaInicio.Value &&
                    s.estado
                select new
                {
                    Suscripcion = s,
                    Membresia = m,
                    Persona = p
                }
            ).FirstOrDefaultAsync();

            if (data == null)
                return null;

            data.Suscripcion.fecha_fin = fechaFin.Value;

            await _context.SaveChangesAsync();

            return new SuscripcionDTO
            {
                ci = data.Persona.ci,
                codigo = data.Membresia.codigo,
                nombre = data.Membresia.nombre,
                fecha_inicio = data.Suscripcion.fecha_inicio,
                fecha_fin = data.Suscripcion.fecha_fin
            };
        }

        public async Task<SuscripcionDTO?> DeleteSuscripcion(
            string ci,
            string codigo)
        {
            var data = await (
                from s in _context.Suscripciones
                join c in _context.Clientes
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias
                    on s.id_membresia equals m.id_membresia
                where
                    p.ci == ci &&
                    m.codigo == codigo &&
                    s.estado
                select new
                {
                    Suscripcion = s,
                    Membresia = m,
                    Persona = p
                }
            ).FirstOrDefaultAsync();

            if (data == null)
                return null;

            var dto = new SuscripcionDTO
            {
                ci = data.Persona.ci,
                codigo = data.Membresia.codigo,
                nombre = data.Membresia.nombre,
                fecha_inicio = data.Suscripcion.fecha_inicio,
                fecha_fin = data.Suscripcion.fecha_fin
            };

            data.Suscripcion.estado = false;

            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<SuscripcionDTO?> HabilitarSuscripcion(
            string ci,
            string codigo)
        {
            var data = await (
                from s in _context.Suscripciones
                join c in _context.Clientes
                    on s.id_cliente equals c.id_cliente
                join p in _context.Personas
                    on c.id_persona equals p.id_persona
                join m in _context.Membresias
                    on s.id_membresia equals m.id_membresia
                where
                    p.ci == ci &&
                    m.codigo == codigo &&
                    !s.estado
                select new
                {
                    Suscripcion = s,
                    Membresia = m,
                    Persona = p
                }
            ).FirstOrDefaultAsync();

            if (data == null)
                return null;

            data.Suscripcion.estado = true;

            await _context.SaveChangesAsync();

            return new SuscripcionDTO
            {
                ci = data.Persona.ci,
                codigo = data.Membresia.codigo,
                nombre = data.Membresia.nombre,
                fecha_inicio = data.Suscripcion.fecha_inicio,
                fecha_fin = data.Suscripcion.fecha_fin
            };
        }
    }
}