using DTOS;
using Models;

namespace Mapeador
{
    public static class CargoSalarioMapeador
    {
        public static CargoSalarioDTO toCargoSalarioDTO (this CargoSalario car)
        {
            return new CargoSalarioDTO()
            {
                Cargo = car.cargo.Nombre,
                Salario = car.salario.Salarioo,
                FechaFin = car.Fecha_Fin,
                FechaInicio = car.Fecha_Inicio
            };
        }
    }
}