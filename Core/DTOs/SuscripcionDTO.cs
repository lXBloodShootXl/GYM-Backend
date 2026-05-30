namespace GYM.Core.DTOs
{
    public class SuscripcionDTO
    {
        public string ci {  get; set; } = null!;
        public string codigo { get; set; } = null!;
        public string nombre { get; set; } = null!; //nombre de membresia
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
    }
}