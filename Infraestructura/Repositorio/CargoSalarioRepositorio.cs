using DTOS;
using GYM.Core.Mapedores;
using GYM.Infraestructura.Data;
using Interfaces;
using Mapeador;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositorios
{
    public class CargoSalarioRepositorio : ICargoSalarioRepositorio
    {
        private readonly GYM_DBContext _context;
        public CargoSalarioRepositorio (GYM_DBContext context)
        {
            _context = context;
        }

        public async Task<List<CargoSalarioDTO>> GetCargoSalario()
        {
            return await(from cs in _context.CargoSalario
                        join ca in _context.Cargo on cs.Id_Cargo equals ca.Cargo_Id
                        join sa in _context.Salario on cs.Id_Salario equals sa.Salario_Id
                        select new CargoSalarioDTO
                        {
                            Cargo = ca.Nombre,
                            Salario = sa.Salarioo,
                            FechaInicio = cs.Fecha_Inicio,
                            FechaFin = cs.Fecha_Fin
                        }).ToListAsync();
        }

        public async Task<CargoSalarioDTO> POSTCargoSalario(string codigoSalario, string codigoCArgo, string FechaInicio, string FechaFin)
        {
            Cargo? cargo = await(from car in _context.Cargo
                                 where car.Codigo == codigoCArgo
                                 select car).FirstOrDefaultAsync();
            Salario? salario = await (from role in _context.Salario
                            where role.Codigo == codigoSalario
                            select role).FirstOrDefaultAsync();
            if(cargo == null ||  salario == null)
            {
                return null;
            }
            var cargoSalario =await (from rh in _context.CargoSalario
                                    where rh.Id_Cargo == cargo.Cargo_Id && rh.Id_Salario == salario.Salario_Id
                                    select rh).FirstOrDefaultAsync();
            if(cargoSalario != null)
            {
                return null;
            }
            var fechaIni = FechaInicio.toDateOnly();
            var fechaFinn = FechaFin.toDateOnly(); 
            var cargosal = new CargoSalario
            {
                Id_Cargo = cargo.Cargo_Id,
                Id_Salario = salario.Salario_Id,
                cargo = cargo,
                salario = salario,
                Fecha_Fin = fechaFinn.Value,
                Fecha_Inicio =fechaIni.Value
            };
            _context.CargoSalario.Add(cargosal);
            await _context.SaveChangesAsync();
            return cargosal.toCargoSalarioDTO();
        }

        public async Task<CargoSalarioDTO> PutCargoSalario(string codigoSalario, string CodigoCargo, string FechaInicio, string FechaFin)
        {
             Cargo? cargo = await(from car in _context.Cargo
                                 where car.Codigo == CodigoCargo
                                 select car).FirstOrDefaultAsync();
            Salario? salario = await (from role in _context.Salario
                            where role.Codigo == codigoSalario
                            select role).FirstOrDefaultAsync();

            if(cargo == null ||  salario == null)
            {
                return null;
            }

            var cargoSalario = await (from rh in _context.CargoSalario
                                    where rh.Id_Cargo == cargo.Cargo_Id && rh.Id_Salario == salario.Salario_Id
                                    select rh).FirstOrDefaultAsync();
            if(cargoSalario == null)
            {
                throw new Exception ("La relacion no existe");
            }
            var fechaIni = FechaInicio.toDateOnly();
            var fechaFinn = FechaFin.toDateOnly();
            cargoSalario.Fecha_Fin=fechaFinn.Value;
            cargoSalario.Fecha_Inicio=fechaIni.Value;
            await _context.SaveChangesAsync();
            return cargoSalario.toCargoSalarioDTO();
        }
    }

}