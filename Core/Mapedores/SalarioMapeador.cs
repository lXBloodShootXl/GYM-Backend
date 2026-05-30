using DTOS;
using Models;

namespace Mapeador
{
    public static class SalarioMapeador
    {
        public static SalarioDTO toSalarioDTO (this Salario salario)
        {
            return new SalarioDTO()
            {
              Codigo = salario.Codigo,
              Salarioo = salario.Salarioo  
            };
        }
    }
}