using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;

namespace GYM.Infraestructura.Repositorio
{
    public class TelefonoRepositorio : ITelefonoRepositorio
    {
        private readonly GYM_DBContext _context;

        public TelefonoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<TelefonoDTO?> GetTelefono(string telf)
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.telf == telf && p.estado != false)
                .Select(p => p.toTelefonoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<TelefonoDTO>> GetTelefono()
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.estado != false)
                .Select(p => p.toTelefonoDTO())
                .ToListAsync();
        }

        public async Task<List<TelefonoDTO>> GetTelefonoBorrados()
        {
            return await _context.Telefonos
                .AsNoTracking()
                .Where(p => p.estado == false)
                .Select(p => p.toTelefonoDTO())
                .ToListAsync();
        }

        public async Task<TelefonoDTO> PostTelefono(string telf)
        {
            if (string.IsNullOrWhiteSpace(telf))
                return null;
            var Telefono = new Telefono
            {
                telf = telf,
                estado = true
            };
            _context.Telefonos.Add(Telefono);
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> PutTelefono(string telf, string telf_nuevo)
        {
            if (string.IsNullOrWhiteSpace(telf) || string.IsNullOrWhiteSpace(telf_nuevo))
                return null;
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado != false);
            if (Telefono == null)
                return null;
            Telefono.telf = telf_nuevo;
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> DeleteTelefono(string telf)
        {
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado == true);
            if (Telefono == null) return null;
            Telefono.estado = false;
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }

        public async Task<TelefonoDTO?> HabilitarTelefono(string telf)
        {
            var Telefono = await _context.Telefonos.FirstOrDefaultAsync(p => p.telf == telf && p.estado == false);
            if (Telefono == null) return null;
            Telefono.estado = true;
            await _context.SaveChangesAsync();
            return Telefono.toTelefonoDTO();
        }
    }
}
