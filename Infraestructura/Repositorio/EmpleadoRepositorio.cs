using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Models;

namespace GYM.Infraestructura.Repositorio
{
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly GYM_DBContext _context;

        public EmpleadoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDTO>> GetEmpleado()
        {
            return await _context.Empleados
                .Include(e => e.persona)
                .AsNoTracking()
                .Where(e => e.estado != false &&
                            e.persona.estado != false)
                .Select(e => e.toEmpleadoDTO())
                .ToListAsync();
        }

        public async Task<EmpleadoDTO?> PostEmpleado(string ci, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd))
                return null;

            Persona? persona = await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .FirstOrDefaultAsync();

            if (persona == null)
                return null;

            bool existe = await _context.Empleados
                .Include(e => e.persona)
                .AnyAsync(e =>
                    e.persona.ci == ci &&
                    e.estado != false);

            if (existe)
                return null;

            var empleado = new Empleado
            {
                id_persona = persona.id_persona,
                fecha = DateOnly.FromDateTime(DateTime.Now),
                pwd = pwd,
                estado = true
            };

            await _context.Empleados.AddAsync(empleado);

            await _context.SaveChangesAsync();

            empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e =>
                    e.id_empleado == empleado.id_empleado);

            return empleado?.toEmpleadoDTO();
        }

        public async Task<EmpleadoDTO?> PutEmpleado(string ci, string pwd, string nuevo_pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(pwd) ||
                string.IsNullOrWhiteSpace(nuevo_pwd))
                return null;

            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e =>
                    e.persona.ci == ci &&
                    e.pwd == pwd &&
                    e.estado != false);

            if (empleado == null)
                return null;

            empleado.pwd = nuevo_pwd;

            await _context.SaveChangesAsync();

            return empleado.toEmpleadoDTO();
        }

        public async Task<bool> DeleteEmpleado(string ci)
        {
            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e =>
                    e.persona.ci == ci &&
                    e.estado != false);

            if (empleado == null)
                return false;

            empleado.estado = false;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EmpleadoDTO?> PutEmpleado(string ci, DateOnly fecha)
        {
            var empleado = await _context.Empleados
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e =>
                    e.persona.ci == ci &&
                    e.estado != false);

            if (empleado == null)
                return null;

            empleado.fecha = fecha;

            await _context.SaveChangesAsync();

            return empleado.toEmpleadoDTO();
        }

        public async Task<bool> LoginEmpleado(string ci, string pwd)
        {
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.ci == ci &&
                    p.estado != false);

            if (persona == null)
                return false;

            var login = await _context.Empleados
                .AsNoTracking()
                .AnyAsync(c =>
                    c.id_persona == persona.id_persona &&
                    c.pwd == pwd &&
                    c.estado != false);
            if (!login)
                return false;
            else return true;
        }
    }
}