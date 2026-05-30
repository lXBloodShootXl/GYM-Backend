using DTOS;
using Models;
using Npgsql.Internal;

namespace Mapeador
{
    public static class CargoMapeador
    {
        public static CargoDTO toCargoMapeador(this Cargo cargo)
        {
            return new CargoDTO()
            {
              Codigo = cargo.Codigo,
              Nombre = cargo.Nombre  
            };
        }    
    }
}