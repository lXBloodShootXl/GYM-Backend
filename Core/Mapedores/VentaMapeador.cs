using DTOS;
using GYM.Core.Models;
using Models;

namespace GYM.Core.Mapeadores
{
    public class VentaMapeador
    {
        public static VentasDTO ToDTO(Ventas ventas)
        {
            return new VentasDTO
            {
                codigo = ventas.codigo,
                fecha = ventas.fecha,
                ciEmpleado = ventas.Empleado.persona.ci,
                ciCliente = ventas.Cliente.persona.ci
            };
        }

        public static Ventas ToModel(VentasDTO dto)
        {
            return new Ventas
            {
                codigo = dto.codigo,
                fecha = dto.fecha
            };
        }
    }
}