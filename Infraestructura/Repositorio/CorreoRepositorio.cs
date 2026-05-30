using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;

namespace GYM.Infraestructura.Repositorio
{
    public class CorreoRepositorio : ICorreoRepositorio
    {
        private readonly GYM_DBContext _context;

        public CorreoRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<CorreoDTO?> GetCorreo(string correo)
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.correo == correo && p.estado != false)
                .Select(p => p.toCorreoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<CorreoDTO>> GetCorreo()
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.estado != false)
                .Select(p => p.toCorreoDTO())
                .ToListAsync();
        }

        public async Task<List<CorreoDTO>> GetCorreoBorrados()
        {
            return await _context.Correos
                .AsNoTracking()
                .Where(p => p.estado == false)
                .Select(p => p.toCorreoDTO())
                .ToListAsync();
        }

        public async Task<CorreoDTO> PostCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return null;
            var Correo = new Correo
            {
                correo = correo,
                estado = true
            };
            _context.Correos.Add(Correo);
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> PutCorreo(string correo, string correo_nuevo)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(correo_nuevo))
                return null;
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado != false);
            if (Correo == null)
                return null;
            Correo.correo = correo_nuevo;
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> DeleteCorreo(string correo)
        {
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado == true);
            if (Correo == null) return null;
            Correo.estado = false;
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }

        public async Task<CorreoDTO?> HabilitarCorreo(string correo)
        {
            var Correo = await _context.Correos.FirstOrDefaultAsync(p => p.correo == correo && p.estado == false);
            if (Correo == null) return null;
            Correo.estado = true;
            await _context.SaveChangesAsync();
            return Correo.toCorreoDTO();
        }
    }
}
