using GYM.Core.DTOs;
using GYM.Core.Models;
namespace GYM.Core.Mapedores
{
    public static class ClienteMapeador
    {
        public static ClienteDTO toClienteDTO(this Cliente Cliente,string ci)
        {
            return new ClienteDTO()
            {
                ci = ci,
                fecha = Cliente.fecha
            };
        }
    }
}
