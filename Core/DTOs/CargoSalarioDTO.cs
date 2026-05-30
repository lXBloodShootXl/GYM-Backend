namespace DTOS
{
    public class CargoSalarioDTO
    {
        public string Cargo {get;set;}
        public int Salario {get;set;}
        public DateOnly FechaInicio {get;set;}
        public DateOnly FechaFin {get;set;}
    }
}