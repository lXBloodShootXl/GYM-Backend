using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM.Infraestructura.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly GYM_DBContext _context;

        public PersonaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<PersonaDTO?> GetPersona(string ci)
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .Select(p => p.toPersonaDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<PersonaDTO>> GetPersona()
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.estado != false)
                .Select(p => p.toPersonaDTO())
                .ToListAsync();
        }

        public async Task<List<PersonaDTO>> GetPersonaBorrados()
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.estado == false)
                .Select(p => p.toPersonaDTO())
                .ToListAsync();
        }

        public async Task<PersonaDTO> PostPersona(string ci, string nombre, string? apellido_p, string? apellido_m, bool sexo, string fecha_nacimiento, string hashhuella)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre) || (string.IsNullOrWhiteSpace(apellido_p) && string.IsNullOrWhiteSpace(apellido_m)) || string.IsNullOrWhiteSpace(fecha_nacimiento))
                return null;
            //string --> DateOnly
            var fecha = fecha_nacimiento.toDateOnly();
            if (fecha == null)
                return null;
            var Persona = new Persona
            {
                ci = ci,
                nombre = nombre,
                apellido_p = apellido_p ?? "-1",
                apellido_m = apellido_m ?? "-1",
                sexo = sexo,
                fecha_nacimiento = fecha.Value,
                hashhuella = hashhuella,
                estado = true
            };
            _context.Personas.Add(Persona);
            await _context.SaveChangesAsync();
            return Persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> PutPersona(string ci, string? nombre, string? apellido_p, string? apellido_m, bool? sexo, string? fecha_nacimiento, string? hashhuella)
        {
            if (string.IsNullOrWhiteSpace(ci))
                return null;
            var Persona = await _context.Personas.FirstOrDefaultAsync(p => p.ci == ci && p.estado != false);
            if (Persona == null)
                return null;

            //string --> DateOnly
            var fecha = fecha_nacimiento.toDateOnly();
            if (fecha == null)
                fecha = Persona.fecha_nacimiento;

            Persona.ci = ci;
            Persona.nombre = nombre ?? Persona.nombre;
            Persona.apellido_p = apellido_p ?? Persona.apellido_p;
            Persona.apellido_m = apellido_m ?? Persona.apellido_m;
            Persona.sexo = sexo ?? Persona.sexo;
            Persona.fecha_nacimiento = fecha.Value;
            Persona.hashhuella = hashhuella ?? Persona.hashhuella;
            await _context.SaveChangesAsync();
            return Persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> DeletePersona(string ci)
        {
            var Persona = await _context.Personas.FirstOrDefaultAsync(p => p.ci == ci && p.estado == true);
            if (Persona == null) return null;
            Persona.estado = false;
            await _context.SaveChangesAsync();
            return Persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> HabilitarPersona(string ci)
        {
            var Persona = await _context.Personas.FirstOrDefaultAsync(p => p.ci == ci && p.estado == false);
            if (Persona == null) return null;
            Persona.estado = true;
            await _context.SaveChangesAsync();
            return Persona.toPersonaDTO();
        }
    }
}
