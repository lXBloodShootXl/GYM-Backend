using GYM.Core.Models;

namespace GYM.Core.Interfaces
{
    public interface IAuditoriaRepositorio
    {
        Task<List<Auditoria>> ListaAuditoria();

        Task PostAuditoria(Auditoria auditoria);
    }
}