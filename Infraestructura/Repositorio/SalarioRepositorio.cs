using DTOS;
using GYM.Infraestructura.Data;
using Interfaces;
using Mapeador;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositorios
{
    public class SalarioRepositorio : ISalarioRepositorio
    {
        private readonly GYM_DBContext _context;
        public SalarioRepositorio (GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<SalarioDTO>> GetSalarios()
        {
            return await (from s in _context.Salario
                        where s.estado != false
                        select s).Select(sa => sa.toSalarioDTO()).ToListAsync();

        }
        public async Task<SalarioDTO> PostSalario(string codigo, int salario)
        {
            var existe = await (from e in _context.Salario
                                where e.Codigo == codigo
                                select e).FirstOrDefaultAsync();
            if (existe != null)
            {
                throw new Exception ("Ya existe un salario registrado con ese codigo");
            }
           Salario sal = new Salario()
           {
             Codigo = codigo,
             Salarioo = salario,
             estado = true  
           };
           _context.Salario.Add(sal);
           await _context.SaveChangesAsync();
           return sal.toSalarioDTO();
        }
        public async Task<SalarioDTO> PatchSalario(string codigo, int salario)
        {
            var sal = await (from s in _context.Salario
                            where s.Codigo == codigo
                            select s).FirstOrDefaultAsync();
            if (sal == null)
            {
                throw new Exception ("El salario con ese codigo no existe");
            }
            sal.Codigo = codigo;
            sal.Salarioo = salario;
            await _context.SaveChangesAsync();
            return sal.toSalarioDTO();
        }
        public async Task<SalarioDTO> DeleteSalario(string codigo)
        {
            var sal = await (from s in _context.Salario
                            where s.Codigo == codigo
                            select s).FirstOrDefaultAsync();
            if (sal == null)
            {
                throw new Exception ("El salario con ese codigo no existe");
            }
            sal.estado = false;
            await _context.SaveChangesAsync();
            return sal.toSalarioDTO();
        }

    }

}