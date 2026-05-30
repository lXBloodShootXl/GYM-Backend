using Microsoft.EntityFrameworkCore;
using GYM.Core.DTOs;
using GYM.Core.Interfaces;
using GYM.Core.Mapedores;
using GYM.Core.Models;
using GYM.Infraestructura.Data;

namespace GYM.Infraestructura.Repositorio
{
    public class MembresiaRepositorio : IMembresiaRepositorio
    {
        private readonly GYM_DBContext _context;

        public MembresiaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<MembresiaDTO?> GetMembresia(string codigo)
        {
            return await _context.Membresias
                .AsNoTracking()
                .Where(p => p.codigo == codigo && p.estado != false)
                .Select(p => p.toMembresiaDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<MembresiaDTO>> GetMembresia()
        {
            return await _context.Membresias
                .AsNoTracking()
                .Where(p => p.estado != false)
                .Select(p => p.toMembresiaDTO())
                .ToListAsync();
        }

        public async Task<List<MembresiaDTO>> GetMembresiaBorrados()
        {
            return await _context.Membresias
                .AsNoTracking()
                .Where(p => p.estado == false)
                .Select(p => p.toMembresiaDTO())
                .ToListAsync();
        }

        public async Task<MembresiaDTO> PostMembresia(string codigo, string nombre, int duracion, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre) || duracion < 30 || precio <= 0)
                return null;
            var Membresia = new Membresia
            {
                codigo = codigo,
                nombre = nombre,
                duracion = duracion,
                precio = precio,
                estado = true
            };
            _context.Membresias.Add(Membresia);
            await _context.SaveChangesAsync();
            return Membresia.toMembresiaDTO();
        }

        public async Task<MembresiaDTO?> PutMembresia(string codigo, string? nombre, int? duracion, decimal? precio)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return null;
            var Membresia = await _context.Membresias.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado != false);
            if (Membresia == null)
                return null;
            Membresia.nombre = nombre ?? Membresia.nombre;
            if (duracion != Membresia.duracion && duracion >= 30 && duracion != null)
                Membresia.duracion = duracion.Value;
            else
                Membresia.duracion = Membresia.duracion;


            if (precio != Membresia.precio && precio > 0 && precio != null)
                Membresia.precio = precio.Value;
            else
                Membresia.precio = Membresia.precio;
            await _context.SaveChangesAsync();
            return Membresia.toMembresiaDTO();
        }

        public async Task<MembresiaDTO?> DeleteMembresia(string codigo)
        {
            var Membresia = await _context.Membresias.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == true);
            if (Membresia == null) return null;
            Membresia.estado = false;
            await _context.SaveChangesAsync();
            return Membresia.toMembresiaDTO();
        }

        public async Task<MembresiaDTO?> HabilitarMembresia(string codigo)
        {
            var Membresia = await _context.Membresias.FirstOrDefaultAsync(p => p.codigo == codigo && p.estado == false);
            if (Membresia == null) return null;
            Membresia.estado = true;
            await _context.SaveChangesAsync();
            return Membresia.toMembresiaDTO();
        }
    }
}
