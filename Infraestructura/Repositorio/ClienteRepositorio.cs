using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;

namespace GYM.Infraestructura.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly GYM_DBContext _context;

        public ClienteRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<ClienteDTO?> GetCliente(string ci)
        {
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (persona == null)
                return null;

            return await _context.Clientes
                .AsNoTracking()
                .Where(p => p.id_persona == persona.id_persona && p.estado != false)
                .Select(p => p.toClienteDTO(ci))
                .FirstOrDefaultAsync();
        }

        public async Task<List<ClienteDTO>> GetCliente()
        {
            return await (
                from c in _context.Clientes.AsNoTracking()
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                where c.estado != false && p.estado != false
                select c.toClienteDTO(p.ci)
            ).ToListAsync();
        }

        public async Task<List<ClienteDTO>> GetClienteBorrados()
        {
            return await (
                from c in _context.Clientes.AsNoTracking()
                join p in _context.Personas.AsNoTracking()
                    on c.id_persona equals p.id_persona
                where c.estado != true && p.estado != true
                select c.toClienteDTO(p.ci)
            ).ToListAsync();
        }

        public async Task<ClienteDTO> PostCliente(string ci, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(pwd))
                return null;
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (persona == null)
                return null;
            var Cliente = new Cliente
            {
                id_persona = persona.id_persona,
                fecha = DateOnly.FromDateTime(DateTime.Now),
                pwd = pwd,
                estado = true
            };
            _context.Clientes.Add(Cliente);
            await _context.SaveChangesAsync();
            return Cliente.toClienteDTO(ci);
        }

        public async Task<ClienteDTO?> PutCliente(string ci, string pwd, string nuevo_pwd)
        {
            if (string.IsNullOrWhiteSpace(ci) ||
                string.IsNullOrWhiteSpace(pwd) ||
                string.IsNullOrWhiteSpace(nuevo_pwd))
                return null;

            var cliente = await _context.Clientes
                .Include(e => e.persona)
                .FirstOrDefaultAsync(e =>
                    e.persona.ci == ci &&
                    e.pwd == pwd &&
                    e.estado != false);

            if (cliente == null)
                return null;

            cliente.pwd = nuevo_pwd;

            await _context.SaveChangesAsync();

            return cliente.toClienteDTO(ci);
        }
        public async Task<ClienteDTO?> DeleteCliente(string ci)
        {
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (persona == null)
                return null;

            var Cliente = await _context.Clientes.FirstOrDefaultAsync(p => p.id_persona == persona.id_persona && p.estado == true);
            if (Cliente == null) return null;
            Cliente.estado = false;
            await _context.SaveChangesAsync();
            return Cliente.toClienteDTO(ci);
        }

        public async Task<ClienteDTO?> HabilitarCliente(string ci)
        {
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .Where(p => p.ci == ci && p.estado != false)
                .Select(p => p)
                .FirstOrDefaultAsync();
            if (persona == null)
                return null;
            var Cliente = await _context.Clientes.FirstOrDefaultAsync(p => p.id_persona == persona.id_persona && p.estado == false);
            if (Cliente == null) return null;
            Cliente.estado = true;
            await _context.SaveChangesAsync();
            return Cliente.toClienteDTO(ci);
        }

        public async Task<bool> LoginCliente(string ci, string pwd)
        {
            Persona? persona = await _context.Personas
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.ci == ci &&
                    p.estado != false);

            if (persona == null)
                return false;

            var login = await _context.Clientes
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
