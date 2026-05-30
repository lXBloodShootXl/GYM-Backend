using DTOS;
using Models;

namespace Mapeador
{
    public static class EmpleadoCargoMapeador
    {
        public static EmpleadoCargoDTO toEmpleadoCargoDTO (this EmpleadoCargo em)
        {
            return new EmpleadoCargoDTO()
            {
                FechaFin = em.FechaFin,
                FechaIncio = em.FechaIncio  
            };
        }
    }
}