using DTOS;
using GYM.Infraestructura.Data;
using Interfaces;
using Mapeador;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositorios
{
    public class CargoRepositorio : ICargoRepositorio
    {
        private readonly GYM_DBContext _context;
        public CargoRepositorio (GYM_DBContext context)
        {
            _context = context;
        }
        public async Task<List<CargoDTO>> GetCargos()
        {
            return await (from s in _context.Cargo
                        where s.estado != false
                        select s).Select(sa => sa.toCargoMapeador()).ToListAsync();
        }
        public async Task<CargoDTO> POSTCargos(string codigo, string nombre)
        {
            var existe = await (from e in _context.Cargo
                                where e.Codigo == codigo
                                select e).FirstOrDefaultAsync();
            if (existe != null)
            {
                throw new Exception ("Ya existe un Cargo registrado con ese codigo");
            }
           Cargo cargo = new Cargo()
           {
             Codigo = codigo,
             Nombre = nombre,
             estado = true  
           };
           _context.Cargo.Add(cargo);
           await _context.SaveChangesAsync();
           return cargo.toCargoMapeador();
        }
        public async Task<CargoDTO> PatchCargos(string codigo, string nombre)
        {
            var cargo = await (from s in _context.Cargo
                            where s.Codigo == codigo
                            select s).FirstOrDefaultAsync();
            if (cargo == null)
            {
                throw new Exception ("El Cargo con ese codigo no existe");
            }
            cargo.Codigo = codigo;
            cargo.Nombre = nombre;
            await _context.SaveChangesAsync();
            return cargo.toCargoMapeador();
        }

        public async Task<CargoDTO> DeleteCargos(string codigo)
        {
            var cargo = await (from s in _context.Cargo
                            where s.Codigo == codigo
                            select s).FirstOrDefaultAsync();
            if (cargo == null)
            {
                throw new Exception ("El salario con ese codigo no existe");
            }
            cargo.estado = false;
            await _context.SaveChangesAsync();
            return cargo.toCargoMapeador();
        }
    }

}