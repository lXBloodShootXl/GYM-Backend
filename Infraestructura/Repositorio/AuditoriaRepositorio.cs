using GYM.Core.Interfaces;
using GYM.Core.Models;
using GYM.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM.Infraestructura.Repositorio
{
    public class AuditoriaRepositorio : IAuditoriaRepositorio
    {
        private readonly GYM_DBContext _context;

        public AuditoriaRepositorio(GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<Auditoria>> ListaAuditoria()
        {
            return await _context.Auditorias.ToListAsync();
        }

        public async Task PostAuditoria(Auditoria auditoria)
        {
            await _context.Auditorias.AddAsync(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}