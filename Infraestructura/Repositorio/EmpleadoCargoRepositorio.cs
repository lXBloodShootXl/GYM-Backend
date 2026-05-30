using DTOS;
using GYM.Core.Mapedores;
using GYM.Infraestructura.Data;
using Interfaces;
using Mapeador;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositorios
{
    public class EmpleadoCargoRepositorio : IEmpleadoCargoRepositorio
    {
        private readonly GYM_DBContext _context;
        public EmpleadoCargoRepositorio (GYM_DBContext context)
        {
            _context = context;
        }
        public async Task<List<EmpleadoCargoDTO>> GetEmpleadoCargoSalario()
        {
             return await(from ec in _context.EmpleadoCargo
                        join ca in _context.Cargo on ec.Id_Cargo equals ca.Cargo_Id
                        join em in _context.Empleados on ec.Id_Empleado equals em.id_empleado
                        where ec.Estado == true
                        select new EmpleadoCargoDTO
                        {
                            Nombre = em.persona.nombre,
                            Cargo = ca.Nombre,
                            FechaFin=ec.FechaFin,
                            FechaIncio = ec.FechaIncio
                        }).ToListAsync();
        }

        public async Task<EmpleadoCargoDTO> POSTEmpleadoCargoSalario(string codigoEmpleado, string codigoCargo, string FechaInicio, string FechaFin)
        {
            Cargo? cargo = await(from car in _context.Cargo
                                 where car.Codigo == codigoCargo
                                 select car).FirstOrDefaultAsync();
            Empleado? empleado = await (from em in _context.Empleados
                            where em.persona.ci == codigoEmpleado
                            select em).FirstOrDefaultAsync();
            if(cargo == null ||  empleado == null)
            {
                return null;
            }
            var cargoEmpleado =await (from ce in _context.EmpleadoCargo
                                    where ce.Id_Cargo == cargo.Cargo_Id && ce.Id_Empleado == empleado.id_empleado
                                    select ce).FirstOrDefaultAsync();
            if(cargoEmpleado != null)
            {
                return null;
            }
            var fechafin = FechaFin.toDateOnly();
            var fechaini = FechaInicio.toDateOnly();
            var cargoEm = new EmpleadoCargo
            {
                Id_Cargo = cargo.Cargo_Id,
                Id_Empleado = empleado.id_empleado,
                cargo = cargo,
                empleado = empleado,
                FechaFin = fechafin.Value,
                FechaIncio = fechaini.Value
            };
            
            _context.EmpleadoCargo.Add(cargoEm);
            await _context.SaveChangesAsync();
            return cargoEm.toEmpleadoCargoDTO();
        }
        public async Task<EmpleadoCargoDTO> PUTEmpleadoCargoSalario(string codigoEmpleado, string codigoCargo, string? FechaInicio, string? FechaFin)
        {
           Cargo? cargo = await(from car in _context.Cargo
                                 where car.Codigo == codigoCargo
                                 select car).FirstOrDefaultAsync();
            Empleado? empleado = await (from em in _context.Empleados
                            where em.persona.ci == codigoEmpleado
                            select em).FirstOrDefaultAsync();
            if(cargo == null ||  empleado == null)
            {
                return null;
            }

            var cargoEmpleado =await (from ce in _context.EmpleadoCargo
                                    where ce.Id_Cargo == cargo.Cargo_Id && ce.Id_Empleado == empleado.id_empleado
                                    select ce).FirstOrDefaultAsync();
            if(cargoEmpleado == null)
            {
                return null;
            }
            var fechafin = FechaFin.toDateOnly();
            var fechaini = FechaInicio.toDateOnly();
            cargoEmpleado.FechaFin=fechafin.Value;
            cargoEmpleado.FechaIncio=fechaini.Value;
            await _context.SaveChangesAsync();
            return cargoEmpleado.toEmpleadoCargoDTO();
        }
        
        public async Task<EmpleadoCargoDTO> DeshabilitarCargo(string codigoEmpleado,string codigoCargo)
        {
            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(c => c.Codigo == codigoCargo);

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(e => e.persona.ci == codigoEmpleado);

            if (cargo == null || empleado == null)
            {
               return null;
            }

            var contrato = await _context.EmpleadoCargo
                .FirstOrDefaultAsync(ec =>
                    ec.Id_Cargo == cargo.Cargo_Id &&
                    ec.Id_Empleado == empleado.id_empleado &&
                    ec.Estado == true);

            if (contrato == null)
            {
                return null;
            }
            contrato.Estado = false;
            await _context.SaveChangesAsync();
            return contrato.toEmpleadoCargoDTO();
        }
    }

}